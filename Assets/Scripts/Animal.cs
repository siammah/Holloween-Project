using UnityEngine;

public class Animal : MonoBehaviour
{
    public Transform player;
    public float roamSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 7f;
    public int damageAmount = 10;
    private Vector3 roamTarget;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        roamTarget = GetRandomRoamPosition();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            // Chase player
            transform.position += (player.position - transform.position).normalized * chaseSpeed * Time.deltaTime;

            // Deal damage
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null && distance < 1f) // adjust 1f for collision range
            {
                ph.TakeDamage(damageAmount);
            }
        }
        else
        {
            // Roam
            transform.position += (roamTarget - transform.position).normalized * roamSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, roamTarget) < 1f)
                roamTarget = GetRandomRoamPosition();
        }
    }

    Vector3 GetRandomRoamPosition()
    {
        return new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50));
    }
}
