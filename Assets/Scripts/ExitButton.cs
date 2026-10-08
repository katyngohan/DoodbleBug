
using UnityEngine;

public class ExitButtonUI : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exiting Doodle Bug...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
