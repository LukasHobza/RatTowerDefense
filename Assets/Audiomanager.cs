using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Odkazy na skripty")]
    public MusicSound musicSound;
    public SFXSounds sfxSounds;

    private void Start()
    {
        // Naèteme uložené hodnoty hlasitosti nebo nastavíme výchozí
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Inicializace hlasitosti pouze pokud objekty nejsou null
        if (musicSound != null)
        {
            SetMusicVolume(musicVolume);
        }
        if (sfxSounds != null)
        {
            InitializeVolume(sfxVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSound != null)
        {
            musicSound.SetVolume(volume);
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSounds != null)
        {
            sfxSounds.SetVolume(volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public float GetSFXVolume()
    {
        return sfxSounds != null ? sfxSounds.GetVolume() : 0f;
    }

    public void InitializeVolume(float volume)
    {
        if (sfxSounds != null)
        {
            SetSFXVolume(volume);
        }
    }
}
