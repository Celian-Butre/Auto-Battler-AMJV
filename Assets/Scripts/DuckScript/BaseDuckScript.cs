using UnityEngine;

public class BaseDuckScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEnemy = true;
    private bool hasCrown = false;
    [SerializeField] GameObject armyManagerEntity;
    void Start()
    {
        ArmyManager armyManagerScript = armyManagerEntity.GetComponent<ArmyManager>();
        armyManagerScript.addTroopToArmy(isEnemy, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
