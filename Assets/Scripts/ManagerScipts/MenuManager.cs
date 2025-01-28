using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject mainPanel; 
    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject levelsPanel;
    [SerializeField] public UnlockedLevelsManager unlockedLevelsManager;
    [SerializeField] public List<GameObject> levelButtons;
    [SerializeField] public GameObject lockPrefab;
    [SerializeField] public GameObject bronzeStarPrefab;
    [SerializeField] public GameObject silverStarPrefab;
    [SerializeField] public GameObject goldStarPrefab;
    [SerializeField] public List<string> levels;
    
    void Start()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelsPanel.SetActive(false);
        
        for (int i = 0; i < levelButtons.Count; i++)
        {
            bool didSpawn = false;
            GameObject spawnedUI = null;
            switch (unlockedLevelsManager.getLevelStatus(i+1))
            {
                case 0:
                    didSpawn = true;
                    spawnedUI = Instantiate(lockPrefab);
                    break;
                case 2:
                    didSpawn = true;
                    spawnedUI = Instantiate(bronzeStarPrefab);
                    break;
                case 3:
                    didSpawn = true;
                    spawnedUI = Instantiate(silverStarPrefab);
                    break;
                case 4:
                    didSpawn = true;
                    spawnedUI = Instantiate(goldStarPrefab);
                    break;
            }

            if (didSpawn)
            {
                spawnedUI.transform.SetParent(levelButtons[i].transform, false);
            }
                
        }
        
    }
    
    public void StartGame()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
        //SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your scene name
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // This won't quit the editor but will work in a built application
        Application.Quit();
    }
    
    public void backToMenu()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelsPanel.SetActive(false);
        Debug.Log("Back To Menu"); // This won't quit the editor but will work in a built application
    }

    public void goToLevel(int level)
    {
        //Debug.Log(unlockedLevelsManager.getLevelStatus(level));
        if (unlockedLevelsManager.getLevelStatus(level) != 0)
        {
            SceneManager.LoadScene(levels[level - 1]);
            Debug.Log("Go To Level " + level);
        }
    }
    
    
}