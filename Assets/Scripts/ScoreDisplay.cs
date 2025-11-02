using UnityEngine;
using TMPro; // remove if not using TMP

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Assign in Inspector

    private void Start()
    {
        // Subscribe to ScoreManager updates
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance.Score);
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
