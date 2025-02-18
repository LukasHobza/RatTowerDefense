using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Slidery pro nastaven� hlasitosti")]
    [SerializeField] private Slider musicSlider; // Slider pro hudbu
    [SerializeField] private Slider sfxSlider;   // Slider pro zvuky entit

    [Header("Odkaz na AudioManager")]
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        // Pokud existuj� ulo�en� hodnoty pro hlasitost, nastav�me je
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        // P�ipojen� metod pro zm�nu hlasitosti p�i zm�n� hodnoty na slideru
        musicSlider.onValueChanged.AddListener((value) =>
        {
            audioManager.SetMusicVolume(value);  // Nastaven� hlasitosti hudby
            PlayerPrefs.SetFloat("MusicVolume", value);
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            audioManager.SetSFXVolume(value);    // Nastaven� hlasitosti SFX zvuk�
            PlayerPrefs.SetFloat("SFXVolume", value);
        });
    }
}
