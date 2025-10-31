using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject endScreenPanel; // assign your panel here

    void Start()
    {
        endScreenPanel.SetActive(false); // hide at start
    }

    public void ShowEndScreen()
    {
        endScreenPanel.SetActive(true); // show full-screen panel
        Time.timeScale = 0f;           // pause the game
    }

    public void HideEndScreen()
    {
        endScreenPanel.SetActive(false);
        Time.timeScale = 1f;           // unpause if needed
    }
}
