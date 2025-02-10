using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit button pressed in the editor.");
#else
            Application.Quit();
#endif
    }
}
