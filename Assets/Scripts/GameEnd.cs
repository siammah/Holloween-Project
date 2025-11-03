using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public static GameEnd Instance { get; private set; }

    [Header("Score Settings")]
    public int score = 0;
    public int winScore = 10;

    private bool gameEnded = false;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        if (gameEnded) return;

        score += points;
        Debug.Log("Score: " + score);

        if (score >= winScore)
        {
            WinGame();
        }
    }

    public void PlayerDied()
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("Player died!");
        SceneManager.LoadScene("DeathScene");
    }

    private void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        Debug.Log("You win!");
        SceneManager.LoadScene("WinScene");
    }

    public void ResetGame()
    {
        score = 0;
        gameEnded = false;
    }
}
