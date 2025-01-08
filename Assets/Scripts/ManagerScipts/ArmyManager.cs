using UnityEngine;
using System.Collections.Generic;

public class ArmyManager : MonoBehaviour
{
    private List<GameObject> enemyArmy = new List<GameObject>();
    private List<GameObject> playerArmy = new List<GameObject>();
    private GameObject enemyCrownDuck;
    private GameObject playerCrownDuck;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Crown + " + enemyCrownDuck);
        Debug.Log("Player Crown + " + playerCrownDuck);
    }

    public List<GameObject> getArmy(bool isEnemy){
        return(isEnemy ? enemyArmy : playerArmy);
    }

    public void addTroopToArmy(bool isEnemy, GameObject Duck){
        (isEnemy ? enemyArmy : playerArmy).Add(Duck);
    }

    public void setCrownDuck(bool isEnemy, GameObject CrownDuck)
    {
        if (isEnemy) {
            enemyCrownDuck = CrownDuck;
        } else {
            playerCrownDuck = CrownDuck;
        }
    }

    public GameObject getCrownDuck(bool isEnemy)
    {
        return(isEnemy ? enemyCrownDuck : playerCrownDuck);
    }
}
