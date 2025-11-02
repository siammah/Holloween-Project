using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    // Called when player presses the button
    public void QuitGame()
    {
        // If you have a main menu scene:
        // SceneManager.LoadScene("MainMenu");

        // Otherwise quit the application
        Application.Quit();
        Debug.Log("Game quit (works only in build, not editor).");
    }
}
