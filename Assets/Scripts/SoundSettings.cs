using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [HideInInspector] public AudioSource source;
    public AudioClip clip;
    public string clipname;
    public bool isLoop;
    public bool playOnAwake;

    [SerializeField] Slider slider;
    [SerializeField] AudioMixer audioMixer;

    public void SetVolume(float value)
    {
        if (value < 1)
        {
            value = 0.01f;
        }
        RefreshSlider(value);
        PlayerPrefs.SetFloat("Saved Maseter Volume", value);
        audioMixer.SetFloat("Master Volume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(slider.value);
    }

    public void RefreshSlider(float value)
    {
        slider.value = value;
    }
}
