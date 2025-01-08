using UnityEngine;
using UnityEngine.AI;
public class BaseDuckScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEnemy = true;
    [SerializeField] bool hasCrown = false;
    [SerializeField] GameObject armyManagerEntity;
    private ArmyManager armyManagerScript;
    [SerializeField] GameObject crownPrefab;
    [SerializeField] int attackMode; //0 do Nothing, 1 attack King
    NavMeshAgent agent;
    Vector3 destination;
    
    void Start()
    {
        armyManagerScript = armyManagerEntity.GetComponent<ArmyManager>();
        armyManagerScript.addTroopToArmy(isEnemy, gameObject);
        agent = GetComponent<NavMeshAgent>();
        
        if (hasCrown){
            becomeCrownDuck();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == 1)
        {
            destination = armyManagerScript.getCrownDuck(!isEnemy).transform.position;
            agent.destination = destination;
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
}
