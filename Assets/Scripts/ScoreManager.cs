using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; } = 0;
    public int winScore = 10;

    void Awake()
    {
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
        Score += points;
        Debug.Log($"Score: {Score}");

        if (Score >= winScore)
        {
            LoadWinScene();
        }
    }

    private void LoadWinScene()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("WinScene");
    }

    public void ResetScore()
    {
        Score = 0;
        Debug.Log("Score reset to 0");
    }
}
