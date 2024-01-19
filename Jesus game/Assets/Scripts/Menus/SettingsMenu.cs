using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    public Slider VolumeInput;
    public TMP_Dropdown GraphicsInput;
    public Toggle FullScreenInput;
    public Dropdown ResolutionInput;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        float volumeSet = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("volume", volumeSet);
        VolumeInput.value = volumeSet;

        int qualitySet = PlayerPrefs.GetInt("Graphics");
        QualitySettings.SetQualityLevel(qualitySet);
        GraphicsInput.value = qualitySet;

        int fullscreenSet = PlayerPrefs.GetInt("FullScreen");
        if (fullscreenSet == 1) {
            Screen.fullScreen = true;
        } else
        {
            Screen.fullScreen = false;
        }
        FullScreenInput.isOn = Screen.fullScreen;

        int resolutionSet = PlayerPrefs.GetInt("Resolution");
        ResolutionInput.value = resolutionSet;
        SetResolution(resolutionSet);


    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
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

    public void SefFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
    }
}
