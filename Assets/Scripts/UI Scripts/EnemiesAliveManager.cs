using UnityEngine;
using TMPro;

public class EnemiesAliveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject enemiesAliveCanvas;
    [SerializeField] GameObject enemiesAliveDisplay;
    private TextMeshProUGUI enemiesAliveMesh;

    private bool switchedCanvasOn = false;

    private bool switchedCanvasOff = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemiesAliveMesh = enemiesAliveDisplay.GetComponent<TextMeshProUGUI>();
        enemiesAliveCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!switchedCanvasOn && gameManager.combatPhase)
        {
            enemiesAliveCanvas.SetActive(true);
            switchedCanvasOn = true;
        }
        
        if (switchedCanvasOn && !switchedCanvasOff && !gameManager.combatPhase)
        {
            enemiesAliveCanvas.SetActive(false);
            switchedCanvasOff = true;
        }

        enemiesAliveMesh.text = "Ennemies En Vie : " + gameManager.armyManager.getArmy(true).Count.ToString();
    }
}
