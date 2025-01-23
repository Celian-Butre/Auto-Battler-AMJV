using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject timerCanvas;
    [SerializeField] GameObject timer;
    private TextMeshProUGUI timerMesh;
    
    [SerializeField] GameObject winTimer;
    private TextMeshProUGUI winTimerMesh;
    
    [SerializeField] GameObject lostTimer;
    private TextMeshProUGUI lostTimerMesh;
    
    

    private bool switchedCanvasOn = false;

    private bool switchedCanvasOff = false;
    private float startTime;

    private float endTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerMesh = timer.GetComponent<TextMeshProUGUI>();
        winTimerMesh = winTimer.GetComponent<TextMeshProUGUI>();
        lostTimerMesh = lostTimer.GetComponent<TextMeshProUGUI>();
        timerCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!switchedCanvasOn && gameManager.combatPhase)
        {
            timerCanvas.SetActive(true);
            switchedCanvasOn = true;
            startTime = Time.time;
        }
        
        if (switchedCanvasOn && !switchedCanvasOff && !gameManager.combatPhase)
        {
            timerCanvas.SetActive(false);
            switchedCanvasOff = true;
            endTime = Time.time-startTime;
            winTimerMesh.text = (((int)(endTime / 60)).ToString() + ":" + ((((int)(endTime) % 60) < 10) ? "0" : "") + ((int)(endTime) % 60).ToString());
            lostTimerMesh.text = (((int)(endTime / 60)).ToString() + ":" + ((((int)(endTime) % 60) < 10) ? "0" : "") + ((int)(endTime) % 60).ToString());
        }

        float combatTime = Time.time - startTime;
        timerMesh.text = (((int)(combatTime / 60)).ToString() + ":" + ((((int)(combatTime) % 60) < 10) ? "0" : "") + ((int)(combatTime) % 60).ToString());
    }
}
