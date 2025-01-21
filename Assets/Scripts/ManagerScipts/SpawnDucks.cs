using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnDucks : MonoBehaviour
{
    [SerializeField] GameObject armyManagerEntity; 
    [SerializeField] GameObject gameManagerEntity;
    private GameManager gameManagerScript;
    [SerializeField] private List<GameObject> duckPrefabs; 
    [SerializeField] GameObject theCamera;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] GameObject troopSelectionCanvas;
    //[SerializeField] private List<Sprite> duckImages;
    [SerializeField] private List<GameObject> troopIcons;
    [SerializeField] private Sprite chosenCadre;
    [SerializeField] private Sprite unchosenCadre;
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
    private GameObject selectedTroop;
    [SerializeField] public GameObject troopEditPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        troopEditPanel.SetActive(false);
        groundLayerMask = LayerMask.GetMask("Dirt") | LayerMask.GetMask("Sand");
        duckLayerMask = LayerMask.GetMask("Duck");
        noSpawnLayerMask = LayerMask.GetMask("Water") | LayerMask.GetMask("Wall");
        currentlySpawningTroop = duckPrefabs[0];
        ActivateDuckCadre(0);
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            deactivateDuckCadre(whichTroopToSpawn);
            whichTroopToSpawn++;
            whichTroopToSpawn = whichTroopToSpawn % troopIcons.Count;
            ActivateDuckCadre(whichTroopToSpawn);
            currentlySpawningTroop = duckPrefabs[whichTroopToSpawn];
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
                    selectedTroop = hitDuck.transform.gameObject;
                    showTroopEditPanel();
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

    public void showTroopEditPanel()
    {
        troopEditPanel.SetActive(true);
    }
    public void despawnSelectedDuck()
    {
        selectedTroop.GetComponent<BaseDuckScript>().despawn();
    }
    
    public void ActivateDuckCadre(int duck)
    {
        troopIcons[duck].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = chosenCadre;
    }

    public void deactivateDuckCadre(int duck)
    {
        troopIcons[duck].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = unchosenCadre;
    }
}
