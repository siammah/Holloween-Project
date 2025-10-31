using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // current score (read-only from other scripts)
    public int Score { get; private set; } = 0;

    // optional: set true if you want ScoreManager to persist between scenes
    public bool persistAcrossScenes = false;

    // event fired when score changes: subscribers receive the new score
    public event Action<int> OnScoreChanged;

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

    // Add points and notify listeners
    public void AddScore(int points)
    {
        if (points == 0) return;
        Score += points;
        Debug.Log($"ScoreManager: Score = {Score}");
        OnScoreChanged?.Invoke(Score);
    }

    // Reset score to zero
    public void ResetScore()
    {
        Score = 0;
        Debug.Log("ScoreManager: Score reset to 0");
        OnScoreChanged?.Invoke(Score);
    }

    // Set score directly (rarely needed)
    public void SetScore(int value)
    {
        Score = value;
        Debug.Log($"ScoreManager: Score set to {Score}");
        OnScoreChanged?.Invoke(Score);
    }
}
