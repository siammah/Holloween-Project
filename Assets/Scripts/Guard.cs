using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public Transform player;
    public float detectionDistance = 8f;
    public float loseChaseDistance = 10f;
    public float attackDistance = 2f;
    public float roamRange = 10f;

    private NavMeshAgent agent;

    public enum State { Roaming, Chasing, Attacking }
    public State currState = State.Roaming;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickRandomDestination();
    }

    void Update()
    {
        switch (currState)
        {
            case State.Roaming:
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    PickRandomDestination();

                if (Vector3.Distance(transform.position, player.position) < detectionDistance)
                    currState = State.Chasing;
                break;

            case State.Chasing:
                agent.SetDestination(player.position);

                if (Vector3.Distance(transform.position, player.position) < attackDistance)
                    currState = State.Attacking;
                else if (Vector3.Distance(transform.position, player.position) > loseChaseDistance)
                    currState = State.Roaming;
                break;

            case State.Attacking:
                agent.SetDestination(transform.position); // stop moving
                // Play attack animation here
                break;
        }
    }

    void PickRandomDestination()
    {
        Vector3 randomPos = transform.position + new Vector3(Random.Range(-roamRange, roamRange), 0, Random.Range(-roamRange, roamRange));
        agent.SetDestination(randomPos);
    }
}


