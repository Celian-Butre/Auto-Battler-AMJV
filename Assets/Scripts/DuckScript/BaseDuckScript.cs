using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;

public class BaseDuckScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEnemy = true;
    [SerializeField] bool hasCrown = false;
    [SerializeField] GameObject armyManagerEntity;
    [SerializeField] GameObject gameManagerEntity;
    private ArmyManager armyManagerScript;
    private GameManager gameManagerScript;
    [SerializeField] GameObject crownPrefab;
    [SerializeField] private int attackMode = 0; //0 do Nothing, 1 offense, 2 Neutre, 3 Défense
    NavMeshAgent agent;
    private Rigidbody duckRB;
    Vector3 destination;
    private float health;
    [SerializeField] private float baseHealth;
    [SerializeField] public GameObject healthBarPrefab;
    [SerializeField] private GameObject healthCanvas;
    private RectTransform healthCanvasRect;
    private GameObject healthBar = null;
    private RectTransform healthBarRect = null;
    private float DuckHeight;
    private GameObject healthBarInside;
    private Image healthBarGradient;
    private float raycastDistance = 1.0f; //for getting ground material
    private LayerMask allGroundLayers;
    [SerializeField] private float baseSpeed;
    private GameObject crown;
    
    void Start()
    {
        health = baseHealth;
        armyManagerScript = armyManagerEntity.GetComponent<ArmyManager>();
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
        armyManagerScript.addTroopToArmy(isEnemy, gameObject);
        agent = GetComponent<NavMeshAgent>();
        duckRB = gameObject.GetComponent<Rigidbody>();
        healthCanvasRect = healthCanvas.GetComponent<RectTransform>();
        
        if (hasCrown){
            becomeCrownDuck();
        }
        
        DuckHeight = GetComponent<Renderer>().bounds.size.y;
        allGroundLayers = LayerMask.GetMask("Dirt") | LayerMask.GetMask("Sand");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.combatPhase)
        {
            updateMovement();
        }
        else
        {
            duckRB.isKinematic = true;
        }

        if (health != baseHealth)
        {
            displayHealthBar();
        }
        
        Vector3 rayOrigin = transform.position;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, allGroundLayers))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Dirt")
            {
                agent.speed = baseSpeed;
            } else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Sand")
            {
                agent.speed = baseSpeed/2;
            }
        }
    }

    public void displayHealthBar()
    {
        if (healthBar == null)
        {
            healthBar = Instantiate(healthBarPrefab, healthCanvas.transform);
            healthBarRect = healthBar.GetComponent<RectTransform>();
            healthBarInside = healthBar.transform.GetChild(1).gameObject;
            healthBarGradient = healthBarInside.GetComponent<Image>();
            healthBarGradient.type = Image.Type.Filled;
            healthBarGradient.fillMethod = Image.FillMethod.Horizontal;
        }

        if (healthBarRect != null) //safety measure 
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);// + new Vector3(0f, 0f, DuckHeight));
            healthBarRect.anchoredPosition = screenPoint + new Vector2(0f, DuckHeight*20);
            healthBarGradient.fillAmount = health / baseHealth;
            healthBarGradient.color = Color.Lerp(Color.red, Color.green, health/baseHealth);
            
        }
        
    }
    public void updateMovement()
    {
        duckRB.isKinematic = false;
        if (attackMode == 1 && armyManagerScript.getCrownDuck(!isEnemy))
        {
            destination = armyManagerScript.getCrownDuck(!isEnemy).transform.position;
            agent.destination = destination;
        }

        if (attackMode == 2)
        {
            List<GameObject> opposingArmy = armyManagerScript.getArmy(!isEnemy);
            if (opposingArmy.Count > 0){
                GameObject closestOpponent = opposingArmy[0];
                float closestDistance = Vector3.Distance(opposingArmy[0].transform.position, transform.position);
                foreach (GameObject opposingDuck in opposingArmy)
                {
                    if (Vector3.Distance(opposingDuck.transform.position, transform.position) < closestDistance)
                    {
                        closestOpponent = opposingDuck;
                        closestDistance = Vector3.Distance(opposingDuck.transform.position, transform.position);
                    }
                }
                destination = closestOpponent.transform.position;
                agent.destination = destination;
            }
        }

        if (attackMode == 3)
        {
            List<GameObject> opposingArmy = armyManagerScript.getArmy(!isEnemy);
            if (opposingArmy.Count > 0){
                GameObject closestOpponent = opposingArmy[0];
                float closestDistance = Vector3.Distance(opposingArmy[0].transform.position, armyManagerScript.getCrownDuck(isEnemy).transform.position);
                foreach (GameObject opposingDuck in opposingArmy)
                {
                    if (Vector3.Distance(opposingDuck.transform.position, armyManagerScript.getCrownDuck(isEnemy).transform.position) < closestDistance)
                    {
                        closestOpponent = opposingDuck;
                        closestDistance = Vector3.Distance(opposingDuck.transform.position, armyManagerScript.getCrownDuck(isEnemy).transform.position);
                    }
                }
                destination = closestOpponent.transform.position;
                agent.destination = destination;
            }
        }
    }

    public void setTeam(bool isOnEnemyTeam){
        isEnemy = isOnEnemyTeam;
    }

    public void giveCrown(){
        hasCrown = true;
    }

    public void setArmyManager(GameObject armyManagerEntity)
    {
        this.armyManagerEntity = armyManagerEntity;
    }

    public ArmyManager getArmyManagerScript()
    {
        return armyManagerScript;
    }
    public void setGameManager(GameObject gameManagerEntity)
    {
        this.gameManagerEntity = gameManagerEntity;
    }

    public void becomeCrownDuck()
    {
        hasCrown = true;
        armyManagerScript.setCrownDuck(isEnemy, gameObject);
        crown = Instantiate(crownPrefab, this.transform);
    }
    
    public void loseMyCrown()
    {
        hasCrown = false;
        armyManagerScript.removeCrownDuck(isEnemy);
        Destroy(crown);
    }

    public void despawn()
    {
        if (hasCrown)
        {
            armyManagerScript.removeCrownDuck(isEnemy);
        }
        armyManagerScript.removeTroopFromArmy(isEnemy, gameObject);
        Destroy(gameObject);
    }
    
    void OnMouseOver()
    {
        // Check if the right mouse button is clicked
        if (Input.GetMouseButtonDown(1)) // 1 is for the right mouse button
        {
            if (!armyManagerScript.getCrownDuck(isEnemy) && !isEnemy)
            {
                becomeCrownDuck();
            }
            else if (armyManagerScript.getCrownDuck(isEnemy) == gameObject && !isEnemy)
            {
                loseMyCrown();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }
    
    public void Heal(float value)
    {
        health += value;
        health = (health > baseHealth) ? baseHealth : health;
    }

    public float getHealth()
    {
        return health;
    }

    public float getBaseHealth()
    {
        return baseHealth;
    }
    private void die()
    {
        armyManagerScript.kill(isEnemy, gameObject, hasCrown);
        Destroy(healthBar);
        Destroy(gameObject);
    }

    public int getAttackMode()
    {
        return (attackMode);
    }

    public bool getTeam()
    {
        return (isEnemy);
    }

    public void setHealthCanvas(GameObject healthCanvas)
    {
        this.healthCanvas = healthCanvas;
    }

    public GameManager getGameManagerScript()
    {
        return(gameManagerScript);
    }
}
