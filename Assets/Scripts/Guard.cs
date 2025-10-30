using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player;
    public Vector3 g = new Vector3(0, -9.81f, 0);
    public float walkSpeed = 20f;
    public float chaseSpeed = 40f;
    public float detectionDistance = 8f;
    public float loseChaseDistance = 10f;
    public float attackDistance = 2f;
    public float time = 0f;
    public float maxTime = 5f;
    public float chargeTime = 3f;
    public float attackPower = 10f;
    public Rigidbody body;
    public Vector3 dir;
    public AudioClip attackSound;
    private AudioSource audioSource;
    public Vector3 v = Vector3.zero;

    public enum State
    {
        Roaming,
        Chasing,
        Whistling,
        Attacking
    }
    public State currState = State.Roaming;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        dir = new Vector3(Random.value, 0, Random.value);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case State.Roaming:
                if(time > maxTime)
                {
                    dir = (new Vector3(-1 + 2 * Random.value, Random.value / walkSpeed, -1 + 2 * Random.value)).normalized;
                    time = 0f;
                }
                time += Time.deltaTime;
                v = walkSpeed * dir;
                detectPlayer();
                break;
            case State.Chasing:
                Vector3 playerDir = player.position - transform.position;
                v = chaseSpeed * playerDir.normalized;
                if (playerDir.magnitude > loseChaseDistance)
                {
                    currState = State.Whistling;
                    Debug.Log("I am now Whistling!");
                    time = 0f;
                    v = Vector3.zero;
                }
                if (playerDir.magnitude < attackDistance)
                {
                    currState = State.Attacking;
                    Debug.Log("I am now Attacking!");
                    time = 0f;
                    v = Vector3.zero;
                }
                break;
            case State.Whistling:
                if(time == 0f)
                {
                    //Send out signal for the whistle, play audio
                }
                else if(time > maxTime)
                {
                    currState = State.Roaming;
                    Debug.Log("I am now Roaming!");
                }
                time += Time.deltaTime;
                break;
            case State.Attacking:
                if(time == 0f)
                {
                    //Play audio for attack, start possible animation
                    audioSource.PlayOneShot(attackSound);

                }
                else if(time > chargeTime)
                {
                    dir = (player.position - transform.position).normalized;
                    v = attackPower * dir;
                    currState = State.Roaming;
                    Debug.Log("I am now Roaming!");
                    time = 0f;
                }
                time += Time.deltaTime;
                break;
        }
        body.velocity = v;
        body.velocity += g;
    }
    public void detectPlayer()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, dir.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.transform == player && Vector3.Angle(player.position - transform.position, dir) < 60)
            {
                currState = State.Chasing;
                Debug.Log("I am now chasing!");
            }
        }
    }
}
