using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool selectionPhase = true;

    public bool combatPhase = false;

    public bool foundWinner = false;

    public bool playerWon;
    [SerializeField] public int currentLevel;
    [SerializeField] public int difficulty;
    [SerializeField] public ArmyManager armyManager;
    [SerializeField] public UnlockedLevelsManager unlockedLevelsManager;
    [SerializeField] public int baseCoins;
    [SerializeField] public int currentCoins;
    [SerializeField] private GameObject coinDisplay;
    private TextMeshProUGUI coinDisplayMesh;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinDisplayMesh = coinDisplay.GetComponent<TextMeshProUGUI>();
        currentCoins = (int) (baseCoins * (difficulty == 1 ? 1.2f : (difficulty == 3 ? 0.8f : 1f)));
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplayMesh.text = currentCoins.ToString();
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

    public void endOfLevel(bool thePlayerWon)
    {
        foundWinner = true;
        playerWon = thePlayerWon;
        unlockedLevelsManager.beatCurrentLevel(currentLevel, difficulty);
    }

    public bool spendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            return true;
        }
        return false;
    }

    public void refundCoins(int amount)
    {
        currentCoins += amount;
    }
}
