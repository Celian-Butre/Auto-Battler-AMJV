using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool difficultySelectionPhase = true;
    public bool spawningPhase = false;

    public bool combatPhase = false;
    public bool endOfGamePhase = false;

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
    [SerializeField] public GameObject healthCanvas;
    [SerializeField] public GameObject wonCanvas;
    [SerializeField] public GameObject lostCanvas;
    [SerializeField] private GameObject coinDisplayCanvas;
    [SerializeField] private GameObject difficultySelectionCanvas;
    [SerializeField] private GameObject troopSelectionCanvas;
    [SerializeField] private RawImage highScoreImage;
    [SerializeField] private RawImage winHighScoreImage;
    [SerializeField] private RawImage loseHighScoreImage;
    [SerializeField] private Texture bronzeSprite;
    [SerializeField] private Texture silverSprite;
    [SerializeField] private Texture goldSprite;
    [SerializeField] private Texture notBeatenSprite;
    [SerializeField] public List<string> levels;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wonCanvas.SetActive(false);
        lostCanvas.SetActive(false);
        coinDisplayCanvas.SetActive(false);
        troopSelectionCanvas.SetActive(false);
        coinDisplayMesh = coinDisplay.GetComponent<TextMeshProUGUI>();
        
        int highScore = unlockedLevelsManager.getLevelStatus(currentLevel);
        switch (highScore)
        {
            case 0:
                highScoreImage.texture = notBeatenSprite;
                break;
            case 1:
                highScoreImage.texture = notBeatenSprite;
                break;
            case 2:
                highScoreImage.texture = bronzeSprite;
                break;
            case 3:
                highScoreImage.texture = silverSprite;
                break;
            case 4:
                highScoreImage.texture = goldSprite;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplayMesh.text = currentCoins.ToString();
        /*
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (spawningPhase)
            {
                startFight();
            }
        }
        */
    }

    public void startFight()
    {
        troopSelectionCanvas.SetActive(false);
        spawningPhase = false;
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

    
    
    public void endOfLevel(bool thePlayerWon)
    {
        combatPhase = false;
        endOfGamePhase = true;
        foundWinner = true;
        playerWon = thePlayerWon;
        coinDisplayCanvas.SetActive(false);
        healthCanvas.SetActive(false);
        if (playerWon)
        {
            unlockedLevelsManager.beatCurrentLevel(currentLevel, difficulty);
            wonCanvas.SetActive(true);
            int highScore = unlockedLevelsManager.getLevelStatus(currentLevel);
            switch (highScore)
            {
                case 0:
                    winHighScoreImage.texture = notBeatenSprite;
                    break;
                case 1:
                    winHighScoreImage.texture = notBeatenSprite;
                    break;
                case 2:
                    winHighScoreImage.texture = bronzeSprite;
                    break;
                case 3:
                    winHighScoreImage.texture = silverSprite;
                    break;
                case 4:
                    winHighScoreImage.texture = goldSprite;
                    break;
            }
        }
        else
        {
            lostCanvas.SetActive(true);
            int highScore = unlockedLevelsManager.getLevelStatus(currentLevel);
            switch (highScore)
            {
                case 0:
                    loseHighScoreImage.texture = notBeatenSprite;
                    break;
                case 1:
                    loseHighScoreImage.texture = notBeatenSprite;
                    break;
                case 2:
                    loseHighScoreImage.texture = bronzeSprite;
                    break;
                case 3:
                    loseHighScoreImage.texture = silverSprite;
                    break;
                case 4:
                    loseHighScoreImage.texture = goldSprite;
                    break;
            }
        }
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

    public void SelectDifficulty(int selectedDifficulty)
    {
        difficultySelectionPhase = false;
        spawningPhase = true;
        difficulty = selectedDifficulty;
        coinDisplayCanvas.SetActive(true);
        troopSelectionCanvas.SetActive(true);
        difficultySelectionCanvas.SetActive(false);
        currentCoins = (int)(baseCoins * (difficulty == 1 ? 1.2f : (difficulty == 3 ? 0.8f : 1f)));
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void goToNextLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
    }

    public void replayLevel()
    {
        SceneManager.LoadScene(levels[currentLevel-1]);
    }
}
