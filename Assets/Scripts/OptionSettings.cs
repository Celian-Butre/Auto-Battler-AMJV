using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSettings : MonoBehaviour
{
    Resolution[] res;
    public TMP_Dropdown resDropdown;

    void Start()
    {
        res = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> resOp = new List<string>();

        int currentRes=0;
        for (int i = 0; i < res.Length; i++)
        {
            string op = res[i].width + " x " + res[i].height;
            resOp.Add(op);

            if (res[i].width == Screen.currentResolution.width && res[i].height==Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resDropdown.AddOptions(resOp);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
        GameObject.Find("Main").SetActive(true);
    }

    public void ChangeQuality(int qindex)
    {
        QualitySettings.SetQualityLevel(qindex);
    }

    public void ChangeResolution(int resIndex)
    {
        Resolution res = Screen.resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
