using UnityEngine;

public class SFXSounds : MonoBehaviour
{
    [Header("AudioSources pro zvuky entit")]
    [SerializeField] private AudioSource shootSound;     // Zvuk st�ely
    [SerializeField] private AudioSource ratdarSound;    // Zvuk radarov�ho za��zen�
    [SerializeField] private AudioSource freezeSound;    // Zvuk zmra�en�

    // Metoda pro nastaven� hlasitosti v�ech zvuk� entit
    public void SetVolume(float volume)
    {
        shootSound.volume = volume;
        ratdarSound.volume = volume;
        freezeSound.volume = volume;
    }

    // Metoda pro z�sk�n� aktu�ln� hlasitosti (pouze pro st�elbu)
    public float GetVolume()
    {
        return shootSound.volume;  // Vrac� aktu�ln� hlasitost
    }

    // Metoda pro inicializaci hlasitosti na specifikovanou hodnotu
    public void InitializeVolume(float volume)
    {
        SetVolume(volume);  // Nastav� hlasitost v�ech zvuk�
    }
}
