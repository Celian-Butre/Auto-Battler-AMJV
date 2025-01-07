using UnityEngine;
using System.Collections.Generic;

public class ArmyManager : MonoBehaviour
{
    private List<GameObject> enemyArmy = new List<GameObject>();
    private List<GameObject> playerArmy = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Enemy Army + " + enemyArmy.Count);
        //Debug.Log("Player Army + " + playerArmy.Count);
    }

    public List<GameObject> getArmy(bool isEnemy){
        return(isEnemy ? enemyArmy : playerArmy);
    }

    public void addTroopToArmy(bool isEnemy, GameObject Duck){
        (isEnemy ? enemyArmy : playerArmy).Add(Duck);
    }
}
