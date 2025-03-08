using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Odkazy na skripty")]
    public MusicSound musicSound;  // Odkaz na skript pro hudbu
    public SFXSounds sfxSounds;    // Odkaz na skript pro zvuky entit

    // Metody pro nastavení hlasitosti hudby a zvukù entit
    public void SetMusicVolume(float volume)
    {
        musicSound.SetVolume(volume);  // Nastavení hlasitosti hudby
    }

    public void SetSFXVolume(float volume)
    {
        sfxSounds.SetVolume(volume);  // Nastavení hlasitosti zvukù entit
    }

    // Getter pro aktuální hlasitost SFX
    public float GetSFXVolume()
    {
        return sfxSounds.GetVolume();  // Opraveno, nyní vrací hodnotu z GetVolume() v SFXSounds
    }

    // Inicializace hlasitosti bez spuštìní radaru
    public void InitializeVolume(float volume)
    {
        sfxSounds.InitializeVolume(volume);
    }
}