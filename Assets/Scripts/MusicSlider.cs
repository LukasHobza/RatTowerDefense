using UnityEngine;

public class MusicSound : MonoBehaviour
{
    [Header("AudioSource pro hudbu")]
    [SerializeField] private AudioSource musicAudioSource; // Hudební AudioSource

    // Metoda pro nastavení hlasitosti hudby
    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}
