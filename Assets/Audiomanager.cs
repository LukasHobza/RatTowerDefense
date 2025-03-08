using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Odkazy na skripty")]
    public MusicSound musicSound;  // Odkaz na skript pro hudbu
    public SFXSounds sfxSounds;    // Odkaz na skript pro zvuky entit

    // Metody pro nastaven� hlasitosti hudby a zvuk� entit
    public void SetMusicVolume(float volume)
    {
        musicSound.SetVolume(volume);  // Nastaven� hlasitosti hudby
    }

    public void SetSFXVolume(float volume)
    {
        sfxSounds.SetVolume(volume);  // Nastaven� hlasitosti zvuk� entit
    }

    // Getter pro aktu�ln� hlasitost SFX
    public float GetSFXVolume()
    {
        return sfxSounds.GetVolume();  // Opraveno, nyn� vrac� hodnotu z GetVolume() v SFXSounds
    }

    // Inicializace hlasitosti bez spu�t�n� radaru
    public void InitializeVolume(float volume)
    {
        sfxSounds.InitializeVolume(volume);
    }
}