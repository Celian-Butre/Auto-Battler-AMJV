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
    [SerializeField] GameObject healthCanvas;
    private LayerMask groundLayerMask;
    private LayerMask duckLayerMask;
    private LayerMask noSpawnLayerMask;
    private bool didHitGround;

    private RaycastHit hitGround;
    private RaycastHit hitDuck;
    private RaycastHit hitNoSpawn;

    private Vector3 directionToMouse;
    private int whichTroopToSpawn = 0;
    private GameObject currentlySpawningTroop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Dirt") | LayerMask.GetMask("Sand");
        duckLayerMask = LayerMask.GetMask("Duck");
        noSpawnLayerMask = LayerMask.GetMask("Water") | LayerMask.GetMask("Wall"); 
        currentlySpawningTroop = greenDuckPrefab;
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            whichTroopToSpawn++;
            whichTroopToSpawn = whichTroopToSpawn % 3;
            switch (whichTroopToSpawn)
            {
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

        if (gameManagerScript.spawningPhase && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            didHitGround = Physics.Raycast(ray, out hitGround, Mathf.Infinity, groundLayerMask);
            bool didHitNoSpawn = Physics.Raycast(ray, out hitNoSpawn, Mathf.Infinity, noSpawnLayerMask);
            bool didHitDuck = Physics.Raycast(ray, out hitDuck, Mathf.Infinity, duckLayerMask);
            if (didHitDuck && (!didHitNoSpawn || hitDuck.distance < hitNoSpawn.distance) && (!didHitGround || hitDuck.distance <= hitGround.distance))
            {
                if (!hitDuck.transform.gameObject.GetComponent<BaseDuckScript>().getTeam())
                {
                    hitDuck.transform.gameObject.GetComponent<BaseDuckScript>().despawn();
                }
            } else if(didHitGround && (!didHitNoSpawn || hitGround.distance < hitNoSpawn.distance) && (!didHitDuck || hitGround.distance < hitDuck.distance)){
                if (currentlySpawningTroop.GetComponent<BaseDuckScript>().cost < gameManagerScript.currentCoins)
                { 
                    GameObject newDuck = Instantiate(currentlySpawningTroop, (hitGround.point + new Vector3(0f, currentlySpawningTroop.GetComponent<Renderer>().bounds.size.y / 2f,0f)), Quaternion.identity);
                    BaseDuckScript duckScript = newDuck.GetComponent<BaseDuckScript>();
                    duckScript.setTeam(false);
                    duckScript.setArmyManager(armyManagerEntity);
                    duckScript.setGameManager(gameManagerEntity);
                    duckScript.setHealthCanvas(healthCanvas);
                    gameManagerScript.spendCoins(duckScript.cost);
                }
                
            }
        }
    }
}
