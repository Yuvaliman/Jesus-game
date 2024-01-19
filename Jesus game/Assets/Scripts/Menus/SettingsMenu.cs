using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider VolumeInput;
    public TMP_Dropdown GraphicsInput;
    public Toggle FullScreenInput;

    private void Start()
    {
        PlayerPrefsSet();
    }

    public void PlayerPrefsSet()
    {
        float volumeSet = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("volume", volumeSet);
        VolumeInput.value = volumeSet;

        int qualitySet = PlayerPrefs.GetInt("Graphics");
        QualitySettings.SetQualityLevel(qualitySet);
        GraphicsInput.value = qualitySet;

        int fullscreenSet = PlayerPrefs.GetInt("FullScreen");
        bool isFullScreen = fullscreenSet == 1;

        // Ensure the UI toggle reflects the correct state
        FullScreenInput.isOn = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        FullScreenInput.isOn = isFullScreen;

        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
