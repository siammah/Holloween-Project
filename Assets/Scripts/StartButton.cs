using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
 
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        // For quitting the game
        Debug.Log("Quit Game"); 
        Application.Quit();
    }
}
