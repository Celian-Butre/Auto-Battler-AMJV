using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] int attackMode; //0 do Nothing, 1 attack King
    NavMeshAgent agent;
    private Rigidbody rigidbody;
    Vector3 destination;
    private float health;
    [SerializeField] private float baseHealth;
    [SerializeField] private float damage;
    
    void Start()
    {
        health = baseHealth;
        armyManagerScript = armyManagerEntity.GetComponent<ArmyManager>();
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
        armyManagerScript.addTroopToArmy(isEnemy, gameObject);
        agent = GetComponent<NavMeshAgent>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        
        if (hasCrown){
            becomeCrownDuck();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.combatPhase)
        {
            rigidbody.isKinematic = false;
            if (attackMode == 1 && armyManagerScript.getCrownDuck(!isEnemy))
            {
                destination = armyManagerScript.getCrownDuck(!isEnemy).transform.position;
                agent.destination = destination;
            }
        }
        else
        {
            rigidbody.isKinematic = true;
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
    public void setGameManager(GameObject gameManagerEntity)
    {
        this.gameManagerEntity = gameManagerEntity;
    }

    private void becomeCrownDuck()
    {
        hasCrown = true;
        armyManagerScript.setCrownDuck(isEnemy, gameObject);
        GameObject crown = Instantiate(crownPrefab, this.transform);
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

    private void die()
    {
        armyManagerScript.removeTroopFromArmy(hasCrown, armyManagerEntity);
        Destroy(gameObject);
        if (hasCrown)
        {
            gameManagerScript.foundWinner = true;
            gameManagerScript.playerWon = isEnemy;
        }
    }
    
}
