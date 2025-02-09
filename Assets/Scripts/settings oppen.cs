using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsOpen : MonoBehaviour 
{
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settiings");
    }
}
