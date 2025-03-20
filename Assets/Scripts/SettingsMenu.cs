using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Slidery pro nastavení hlasitosti")]
    [SerializeField] private Slider musicSlider; // Slider pro hudbu
    [SerializeField] private Slider sfxSlider;   // Slider pro zvuky entit

    [Header("Odkaz na AudioManager")]
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        // Pokud existují uložené hodnoty pro hlasitost, nastavíme je
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        // Inicializace hlasitosti bez spuštìní radaru
        audioManager.InitializeVolume(sfxSlider.value);

        // Pøipojení metod pro zmìnu hlasitosti pøi zmìnì hodnoty na slideru
        musicSlider.onValueChanged.AddListener((value) =>
        {
            audioManager.SetMusicVolume(value);  // Nastavení hlasitosti hudby
            PlayerPrefs.SetFloat("MusicVolume", value);
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            audioManager.SetSFXVolume(value);    // Nastavení hlasitosti SFX zvukù
            PlayerPrefs.SetFloat("SFXVolume", value);
        });
    }

    // Metoda pro získání aktuální hodnoty SFX slideru
    public float GetSFXSliderValue()
    {
        return sfxSlider.value;
    }
}