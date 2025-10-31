using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; } = 0;
    public bool persistAcrossScenes = false;

    public event Action<int> OnScoreChanged;

    // Add this inside the class
    public int winScore = 10; // <-- must be inside the class

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (persistAcrossScenes)
                DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"ScoreManager: Duplicate instance '{gameObject.name}' destroyed.");
            Destroy(gameObject);
            return;
        }
    }

    public void AddScore(int points)
    {
        if (points == 0) return;
        Score += points;
        Debug.Log($"ScoreManager: Score = {Score}");
        OnScoreChanged?.Invoke(Score);

        // Win check (also inside the class)
        if (Score >= winScore)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("You Win!");
        EndScreen endScreen = FindObjectOfType<EndScreen>();
        if (endScreen != null)
            endScreen.ShowEndScreen();
    }


    public void ResetScore()
    {
        Score = 0;
        Debug.Log("ScoreManager: Score reset to 0");
        OnScoreChanged?.Invoke(Score);
    }

    public void SetScore(int value)
    {
        Score = value;
        Debug.Log($"ScoreManager: Score set to {Score}");
        OnScoreChanged?.Invoke(Score);
    }
}
