using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject mainPanel; 
    [SerializeField] public GameObject settingsPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your scene name
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
        Debug.Log("Back To Menu"); // This won't quit the editor but will work in a built application
    }
}