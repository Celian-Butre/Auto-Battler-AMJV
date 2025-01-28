using UnityEngine;

public class PastilleManager : MonoBehaviour
{
    [SerializeField] ArmyManager armyManagerScript;
    [SerializeField] SpawnDucks spawnManagerScript;
    [SerializeField] GameObject enemyPastillePrefab;
    [SerializeField] GameObject playerPastillePrefab;
    [SerializeField] GameObject selectedPastillePrefab;
    [SerializeField] GameManager gameManagerScript;

    private bool removedPastilles = false;
    private bool addedInitialPastilles = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (!addedInitialPastilles && gameManagerScript.spawningPhase)
        {
            addedInitialPastilles = true;
            setEnemyPastilles();
        }

        if (addedInitialPastilles && !removedPastilles && !gameManagerScript.spawningPhase)
        {
            removedPastilles = true;
            removeEnemyPastilles();
            removeTeamPastilles();
        };
    }

    public void setEnemyPastilles()
    {
        foreach (GameObject enemyDuck in armyManagerScript.getArmy(true))
        {
            giveXPastilleToY(enemyPastillePrefab, enemyDuck);
        }
    }

    public void removeTeamPastilles()
    {
        foreach (GameObject playerDuck in armyManagerScript.getArmy(false))
        {
            removeTroopsPastilles(playerDuck);
        }
    }

    public void removeEnemyPastilles()
    {
        foreach (GameObject enemyDuck in armyManagerScript.getArmy(true))
        {
            removeTroopsPastilles(enemyDuck);
        }
    }
    
    

    public void setSelectedPastille(GameObject troop)
    {
        removeTroopsPastilles(troop);
        giveXPastilleToY(selectedPastillePrefab, troop);
    }

    public void setPlayerPastille(GameObject troop)
    {
        removeTroopsPastilles(troop);
        giveXPastilleToY(playerPastillePrefab, troop);
    }

    public void removeTroopsPastilles(GameObject troop)
    {
        Transform PastilleSpawner = troop.transform.Find("pastilleSpawner");
        foreach (Transform child in PastilleSpawner)
        {
            if (child.CompareTag("Pastille"))
            {
                Destroy(child.gameObject); 
            }
        }
    }

    public void giveXPastilleToY(GameObject pastille, GameObject troop)
    {
        Instantiate(pastille, troop.transform.Find("pastilleSpawner").transform);
    }
}
