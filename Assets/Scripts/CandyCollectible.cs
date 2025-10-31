using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCollectible : MonoBehaviour
{
    public int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the ScoreManager and add points
            ScoreManager.Instance.AddScore(scoreValue);
            // Destroy the candy
            Destroy(gameObject);
        }
    }
}
