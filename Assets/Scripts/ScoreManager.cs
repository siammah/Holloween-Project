using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; } = 0;
    public int winScore = 10;

    // Add this event back so ScoreDisplay can subscribe
    public event Action<int> OnScoreChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        Score += points;

        // Notify subscribers
        OnScoreChanged?.Invoke(Score);

        if (Score >= winScore)
        {
            EndScreen endScreen = FindObjectOfType<EndScreen>();
            if (endScreen != null)
                endScreen.ShowMessage("Congrats! You Win!");
        }
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}
