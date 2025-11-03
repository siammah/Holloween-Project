using UnityEngine;
using TMPro; // Remove if you're using regular Text instead

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameEnd.Instance != null)
        {
            scoreText.text = "Score: " + GameEnd.Instance.score;
        }
    }
}
