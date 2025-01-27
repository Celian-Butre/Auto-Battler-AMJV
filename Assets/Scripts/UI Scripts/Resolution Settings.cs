using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions = 
    {
        new Resolution {width = 1280, height = 720},
        new Resolution {width = 1920, height = 1080},
        new Resolution {width = 2560, height = 1440},
        new Resolution {width = 3840, height = 2160}
    };
    private int selectedIndex = 0;
    [SerializeField] private Button applyButton;

    
    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);

        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[selectedIndex];
        Debug.Log(resolution.width + " x " + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionIndex", selectedIndex);
        PlayerPrefs.Save();
    }

    public void preSaveResolution()
    {
        selectedIndex = resolutionDropdown.value;
    }
}