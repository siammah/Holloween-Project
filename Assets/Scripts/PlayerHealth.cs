using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EndScreen endScreen = FindObjectOfType<EndScreen>();
        if (endScreen != null)
            endScreen.ShowMessage("You Died!");

        gameObject.SetActive(false); // disable player
    }
}
