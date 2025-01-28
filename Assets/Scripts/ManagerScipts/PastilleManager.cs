using Unity.VisualScripting;
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

        if (!removedPastilles && !gameManagerScript.spawningPhase)
        {
            Debug.Log("removing");
            removedPastilles = true;
            removeEnemyPastilles();
            removeTeamPastilles();
        };
    }

    public void setEnemyPastilles()
    {
        foreach (GameObject enemyDuck in armyManagerScript.getArmy(true))
        {
            Instantiate(enemyPastillePrefab, enemyDuck.transform);
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
        Instantiate(selectedPastillePrefab, troop.transform);
    }

    public void setPlayerPastille(GameObject troop)
    {
        removeTroopsPastilles(troop);
        Instantiate(playerPastillePrefab, troop.transform);
    }

    public void removeTroopsPastilles(GameObject troop)
    {
        foreach (Transform child in troop.transform)
        {
            if (child.CompareTag("Pastille"))
            {
                Destroy(child.gameObject); 
            }
        }
    }
}
