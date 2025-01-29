using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Start Scene");
    }

    public void LaunchSettings()
    {
        this.gameObject.SetActive(false);
        GameObject.Find("Options").SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
