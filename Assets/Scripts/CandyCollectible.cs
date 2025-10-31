using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCollectible : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip collectSound;
    public float collectVolume = 1f;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

       
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(scoreValue);
        else
            Debug.LogWarning("CandyCollectible: ScoreManager.Instance is null.");

        
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position, collectVolume);

        

        Destroy(gameObject);
    }
}
