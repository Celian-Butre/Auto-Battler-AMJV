using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool selectionPhase = true;

    public bool combatPhase = false;

    public bool foundWinner = false;

    public bool playerWon;
    [SerializeField] public ArmyManager armyManager;
    
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
                if (armyManager.getArmy(true).Count == 0)
                {
                    foundWinner = true;
                    playerWon = true;
                } else if (armyManager.getArmy(false).Count == 0)
                {
                    foundWinner = true;
                    playerWon = false;
                } else {
                    if (!armyManager.getCrownDuck(true))
                    {
                        armyManager.getArmy(true)[0].GetComponent<BaseDuckScript>().becomeCrownDuck();
                    }
                    if (!armyManager.getCrownDuck(false))
                    {
                        armyManager.getArmy(false)[0].GetComponent<BaseDuckScript>().becomeCrownDuck();
                    }
                }
            }
        }
    }
    
    
}
