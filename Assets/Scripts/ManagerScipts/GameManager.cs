using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool selectionPhase = true;

    public bool combatPhase = false;

    public bool foundWinner = false;

    public bool playerWon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectionPhase)
            {
                selectionPhase = false;
                combatPhase = true;
            }
        }
    }
    
    
}
