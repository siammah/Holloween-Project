using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    
    private float currentHealth;
    public GameEnd gameOverManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverManager = FindObjectOfType<GameEnd>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        gameOverManager.PlayerDied();
    }
}
