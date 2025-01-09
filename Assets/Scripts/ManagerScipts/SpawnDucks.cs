using UnityEngine;

public class SpawnDucks : MonoBehaviour
{
    [SerializeField] GameObject armyManagerEntity; 
    [SerializeField] GameObject gameManagerEntity;
    private GameManager gameManagerScript;
    [SerializeField] GameObject greenDuckPrefab;
    [SerializeField] GameObject blueDuckPrefab;
    [SerializeField] GameObject yellowDuckPrefab;
    [SerializeField] GameObject theCamera;
    LayerMask layerMask;
    private bool didHit;

    private RaycastHit hit;

    private Vector3 directionToMouse;
    private int whichTroopToSpawn = 0;
    private GameObject currentlySpawningTroop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        currentlySpawningTroop = greenDuckPrefab;
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            whichTroopToSpawn++;
            whichTroopToSpawn = whichTroopToSpawn % 3;
            switch (whichTroopToSpawn){
                case 0:
                    currentlySpawningTroop = greenDuckPrefab;
                    break;
                case 1:
                    currentlySpawningTroop = blueDuckPrefab;
                    break;
                case 2:
                    currentlySpawningTroop = yellowDuckPrefab;
                    break;
            }
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        didHit = (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask));

        if (gameManagerScript.selectionPhase && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Left mouse button clicked!")
            if(didHit){
                GameObject newDuck = Instantiate(currentlySpawningTroop, (hit.point + new Vector3(0f, currentlySpawningTroop.GetComponent<Renderer>().bounds.size.y / 2f,0f)), Quaternion.identity);
                BaseDuckScript duckScript = newDuck.GetComponent<BaseDuckScript>();
                duckScript.setTeam(false);
                duckScript.setArmyManager(armyManagerEntity);
                duckScript.setGameManager(gameManagerEntity);
            }
        }
    }
}
