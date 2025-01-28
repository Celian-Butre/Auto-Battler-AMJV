using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SpawnDucks : MonoBehaviour
{
    [SerializeField] GameObject armyManagerEntity; 
    [SerializeField] GameObject gameManagerEntity;
    private GameManager gameManagerScript;
    private ArmyManager armyManagerScript;
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
    [SerializeField] private Button crownButton;
    [SerializeField] private Sprite hasCrownButton;
    [SerializeField] private Sprite noCrownButton;
    
    [SerializeField] private Button offenseModeButton;
    [SerializeField] private Button randomModeButton;
    [SerializeField] private Button defenseModeButton;

    [SerializeField] private Sprite offenseModeOff;
    [SerializeField] private Sprite offenseModeOn;
    [SerializeField] private Sprite randomModeOff;
    [SerializeField] private Sprite randomModeOn;
    [SerializeField] private Sprite defenseModeOff;
    [SerializeField] private Sprite defenseModeOn;
    private BaseDuckScript selectedTroopScript;
    [SerializeField] private PastilleManager pastilleManager;

    [SerializeField] private TextMeshProUGUI priceTagMesh;
    [SerializeField] private TextMeshProUGUI healthTagMesh;
    [SerializeField] private TextMeshProUGUI movSpeedTagMesh;
    [SerializeField] private TextMeshProUGUI armorTagMesh;
    [SerializeField] private TextMeshProUGUI damageTagMesh;
    [SerializeField] private TextMeshProUGUI attackSpeedTagMesh;
    [SerializeField] private TextMeshProUGUI descriptionTagMesh;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        troopEditPanel.SetActive(false);
        groundLayerMask = LayerMask.GetMask("Dirt") | LayerMask.GetMask("Sand");
        duckLayerMask = LayerMask.GetMask("Duck");
        noSpawnLayerMask = LayerMask.GetMask("Water") | LayerMask.GetMask("Wall");
        currentlySpawningTroop = duckPrefabs[0];
        ActivateDuckCadre(0);
        updateTroopStats();
        gameManagerScript = gameManagerEntity.GetComponent<GameManager>();
        armyManagerScript = armyManagerEntity.GetComponent<ArmyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        choseIfShowTroopEditPanel();
        if (Input.GetKeyDown(KeyCode.C))
        {
            deactivateDuckCadre(whichTroopToSpawn);
            whichTroopToSpawn++;
            whichTroopToSpawn = whichTroopToSpawn % troopIcons.Count;
            ActivateDuckCadre(whichTroopToSpawn);
            currentlySpawningTroop = duckPrefabs[whichTroopToSpawn];
            updateTroopStats();
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
                    setSelectedTroop(hitDuck.transform.gameObject);
                }
            } else if(didHitGround && (!didHitNoSpawn || hitGround.distance < hitNoSpawn.distance) && (!didHitDuck || hitGround.distance < hitDuck.distance)){
                if (currentlySpawningTroop.GetComponent<BaseDuckScript>().cost <= gameManagerScript.currentCoins)
                { 
                    GameObject newDuck = Instantiate(currentlySpawningTroop, (hitGround.point + new Vector3(0f, currentlySpawningTroop.transform.Find("TigeUI").GetComponent<Renderer>().bounds.size.y * 0.8f,0f)), Quaternion.identity);
                    BaseDuckScript duckScript = newDuck.GetComponent<BaseDuckScript>();
                    duckScript.setTeam(false);
                    duckScript.setArmyManager(armyManagerEntity);
                    duckScript.setGameManager(gameManagerEntity);
                    duckScript.setHealthCanvas(healthCanvas);
                    gameManagerScript.spendCoins(duckScript.cost);
                    setSelectedTroop(newDuck);
                }
                
            }
        }
    }

    public void updateTroopStats()
    {
        List<string> troopStats = currentlySpawningTroop.GetComponent<BaseDuckScript>().troopStats;
        priceTagMesh.text = "Prix : " + troopStats[0]; 
        healthTagMesh.text = "PV : " + troopStats[1];
        movSpeedTagMesh.text = "Vitesse (Déplacement) : " + troopStats[2];
        armorTagMesh.text = "Armure : " + troopStats[3];
        damageTagMesh.text = "Dégats : " + troopStats[4];
        attackSpeedTagMesh.text = "Vitesse (Attaque) : " + troopStats[5];
        descriptionTagMesh.text = "Description : " + troopStats[6];
    }

    public void choseIfShowTroopEditPanel()
    {
        if (selectedTroop != null)
        {
            troopEditPanel.SetActive(true);
            crownButton.image.sprite = (selectedTroopScript.hasCrown) ? hasCrownButton : noCrownButton;
            offenseModeButton.image.sprite = ((selectedTroopScript.getAttackMode() == 1) ? offenseModeOn : offenseModeOff);
            randomModeButton.image.sprite = ((selectedTroopScript.getAttackMode() == 2) ? randomModeOn : randomModeOff);
            defenseModeButton.image.sprite = ((selectedTroopScript.getAttackMode() == 3) ? defenseModeOn : defenseModeOff);
        }
        else
        {
            troopEditPanel.SetActive(false);
        }
        
    }
    public void despawnSelectedDuck()
    {
        selectedTroopScript.despawn();
        selectedTroop = null;
    }
    
    public void ActivateDuckCadre(int duck)
    {
        troopIcons[duck].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = chosenCadre;
    }

    public void deactivateDuckCadre(int duck)
    {
        troopIcons[duck].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = unchosenCadre;
    }

    public void giveCrownToSelected()
    {
        armyManagerScript.giveCrownDuckTo(false, selectedTroop);
    }

    public void removeCrownFromSelected()
    {
        armyManagerScript.removeCrownDuckFrom(false, selectedTroop);
    }

    public void toggleCrownFromSelected()
    {
        if (selectedTroopScript.hasCrown)
        {
            removeCrownFromSelected();
        }
        else
        {
            giveCrownToSelected();
        }
    }
    
    public void setSelectedDuckMode(int mode)
    {
        selectedTroopScript.setAttackMode(mode);
    }

    public void setSelectedTroop(GameObject troop)
    {
        if (selectedTroop != null)
        {
            pastilleManager.setPlayerPastille(selectedTroop);
        }
        selectedTroop = troop;
        selectedTroopScript = selectedTroop.GetComponent<BaseDuckScript>();
        pastilleManager.setSelectedPastille(troop);
    }
}
