using UnityEngine;

public class SFXSounds : MonoBehaviour
{
    [Header("AudioSources pro zvuky entit")]
    [SerializeField] private AudioSource shootSound;     // Zvuk støely
    [SerializeField] private AudioSource ratdarSound;    // Zvuk radarového zaøízení
    [SerializeField] private AudioSource freezeSound;    // Zvuk zmražení

    // Metoda pro nastavení hlasitosti všech zvukù entit
    public void SetVolume(float volume)
    {
        shootSound.volume = volume;
        ratdarSound.volume = volume;
        freezeSound.volume = volume;
    }

    // Metoda pro získání aktuální hlasitosti (pouze pro støelbu)
    public float GetVolume()
    {
        return shootSound.volume;  // Vrací aktuální hlasitost
    }

    // Metoda pro inicializaci hlasitosti na specifikovanou hodnotu
    public void InitializeVolume(float volume)
    {
        SetVolume(volume);  // Nastaví hlasitost všech zvukù
    }
}
