using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 10;   
    public float damageCooldown = 1f;     
    private bool canDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
