using UnityEngine;

public class MusicSound : MonoBehaviour
{
    [Header("AudioSource pro hudbu")]
    [SerializeField] private AudioSource musicAudioSource; // Hudebn� AudioSource

    // Metoda pro nastaven� hlasitosti hudby
    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}
