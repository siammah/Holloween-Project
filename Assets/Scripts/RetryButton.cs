using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private string mainGameScene = "SampleScene"; // your main scene name

    // This function is called when the button is clicked
    public void TryAgain()
    {
        Debug.Log("Retry clicked, loading scene: " + mainGameScene);
        SceneManager.LoadScene(mainGameScene, LoadSceneMode.Single);
    }

    private void Awake()
    {
        // Ensure the button GameObject is active when the scene loads
        gameObject.SetActive(true);
    }
}
