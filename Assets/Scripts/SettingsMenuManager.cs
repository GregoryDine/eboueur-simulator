using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Dropdown qualityDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Dropdown keybindsDropdown;

    Resolution[] resolutions;

    public int keybindsProfile;

    public static SettingsMenuManager instance;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple SettingsMenuManager instances!");
            return;
        }

        instance = this;
    }

    public void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume");
        if (savedVolume == 0)
        {
            savedVolume = 1f;
        }
        volumeSlider.value = savedVolume;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(savedVolume) * 20);

        keybindsProfile = PlayerPrefs.GetInt("keybindsProfile");
        keybindsDropdown.value = keybindsProfile;

        fullscreenToggle.isOn = Screen.fullScreen;

        qualityDropdown.value = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetKeybindsProfile(int keybindsIndex)
    {
        keybindsProfile = keybindsIndex;
        PlayerPrefs.SetInt("keybindsProfile", keybindsProfile);
    }

    public void BackButton()
    {
        settingsMenu.SetActive(false);
    }
}