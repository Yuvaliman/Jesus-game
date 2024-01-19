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

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);

        // Log current resolution and selected index
        Debug.Log($"Current Resolution: {Screen.currentResolution.width} X {Screen.currentResolution.height}");
        Debug.Log($"Selected Resolution Index: {currentResolutionIndex}");

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

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
        if (fullscreenSet == 1)
        {
            Screen.fullScreen = true;
            FullScreenInput.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            FullScreenInput.isOn = false;
        }

        int resolutionSet = PlayerPrefs.GetInt("Resolution");

        // Ensure the saved resolution index is within bounds
        resolutionSet = Mathf.Clamp(resolutionSet, 0, resolutions.Length - 1);

        resolutionDropdown.value = resolutionSet;

        // Log selected resolution index
        Debug.Log($"Selected Resolution Index on PlayerPrefsSet: {resolutionSet}");

        Resolution resolution = resolutions[resolutionSet];

        // Log selected resolution
        Debug.Log($"Selected Resolution on PlayerPrefsSet: {resolution.width} X {resolution.height}");

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolution()
    {
        int resolutionIndex = resolutionDropdown.value;
        Resolution resolution = resolutions[resolutionIndex];

        // Log selected resolution
        Debug.Log($"Selected Resolution on SetResolution: {resolution.width} X {resolution.height}");

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
        PlayerPrefsSet();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefsSet();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);
        PlayerPrefsSet();
    }

    public void SetFullScreen()
    {
        bool isFullScreen = FullScreenInput.GetComponent<Toggle>().isOn;

        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
        PlayerPrefsSet();
    }
}
