using UnityEngine;

public class SFXSounds : MonoBehaviour
{
    [Header("AudioSources pro zvuky entit")]
    [SerializeField] private AudioSource shootSound;     // Zvuk střely
    [SerializeField] private AudioSource ratdarSound;    // Zvuk radarového zařízení
    [SerializeField] private AudioSource freezeSound;    // Zvuk zmražení

    // Metoda pro nastavení hlasitosti všech zvuků entit
    public void SetVolume(float volume)
    {
        Debug.Log("Nastavuji hlasitost na: " + volume);
        shootSound.volume = volume;
        ratdarSound.volume = volume;
        freezeSound.volume = volume;
    }

    // Metoda pro získání aktuální hlasitosti (pouze pro střelbu)
    public float GetVolume()
    {
        return shootSound.volume;  // Vrací aktuální hlasitost
    }

    // Metoda pro inicializaci hlasitosti na specifikovanou hodnotu
    public void InitializeVolume(float volume)
    {
        SetVolume(volume);  // Nastaví hlasitost všech zvuků
    }
    
}


