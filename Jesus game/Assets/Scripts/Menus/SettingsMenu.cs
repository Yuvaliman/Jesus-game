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

        // Clear dropdown options
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = -1;

        foreach (Resolution resolution in resolutions)
        {
            if (Is16By9AspectRatio(resolution))
            {
                string option = $"{resolution.width} X {resolution.height}";

                // Avoid adding duplicate resolutions
                if (!resolutionDropdown.options.Contains(new Dropdown.OptionData(option)))
                {
                    resolutionDropdown.options.Add(new Dropdown.OptionData(option));

                    // Find the current resolution index
                    if (Screen.width == resolution.width && Screen.height == resolution.height)
                    {
                        currentResolutionIndex = resolutionDropdown.options.Count - 1;
                    }
                }
            }
        }

        // Set the dropdown value to the current resolution index
        resolutionDropdown.value = currentResolutionIndex != -1 ? currentResolutionIndex : 0;
        resolutionDropdown.RefreshShownValue();

        PlayerPrefsSet();
    }

    // Helper method to check if a resolution has a 16:9 aspect ratio
    private bool Is16By9AspectRatio(Resolution resolution)
    {
        return Mathf.Approximately((float)resolution.width / resolution.height, 16f / 9f);
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

        ApplyResolution();
    }

    public void ApplyResolution()
    {
        int resolutionIndex = resolutionDropdown.value;
        Resolution resolution = resolutions[resolutionIndex];

        if (FullScreenInput.isOn)
        {
            // Set fullscreen mode with screen scaling mode to match display's native resolution
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            // Set windowed mode
            Screen.SetResolution(resolution.width, resolution.height, false);
        }

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

    public void SetFullScreen(bool isFullScreen)
    {
        FullScreenInput.isOn = isFullScreen;

        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);

        // Set the resolution based on the toggle state
        ApplyResolution();
    }
}
