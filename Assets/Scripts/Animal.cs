using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Animal : MonoBehaviour
{
    static public Vector3 honeLocation = Vector3.zero;
    public Vector3 oldHoneLocation = Vector3.zero;
    public enum State
    {
        Roaming,
        Honing,
        Chasing
    }
    public GameObject Guard;
    public Transform Player;
    public State currState = State.Roaming;
    public Vector3 roamLocation;
    public float time = 0f;
    public float maxTime = 10f;
    public float tooClose = 7f;
    public float tooFar = 12f;
    public float roamSpeed = 2f;
    public float chaseSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Guard = transform.parent.GetChild(0).gameObject;
        roamLocation = new Vector3(Random.Range(-50, 50), Random.Range(1, 30), Random.Range(-50, 50));
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case State.Roaming:
                if(time > maxTime)
                {
                    roamLocation = new Vector3(Random.Range(-50, 50), Random.Range(1, 30), Random.Range(-50, 50));
                    time = 0f;
                }
                time += Time.deltaTime;
                transform.position += (roamLocation - transform.position).normalized * roamSpeed * Time.deltaTime;
                detectPlayer();
                break;
            case State.Honing:
                if(time > maxTime)
                {
                    currState = State.Roaming;
                }
                time += Time.deltaTime;
                transform.position += (honeLocation - transform.position).normalized * chaseSpeed * Time.deltaTime;
                detectPlayer();
                break;
            case State.Chasing:
                Vector3 playerPos = Player.position - transform.position;
                transform.position += playerPos.normalized * chaseSpeed * Time.deltaTime;
                if (playerPos.magnitude > tooFar)
                {
                    currState = State.Roaming;
                }
                break;
        }
        detectHoning();
    }
    void detectPlayer()
    {
        Vector3 distance = Player.position - transform.position;
        if(distance.magnitude < tooClose)
        {
            currState = State.Chasing;
        }
    }
    void detectHoning()
    {
        Debug.Log("Honing");
        Debug.Log(honeLocation);
        if (oldHoneLocation != honeLocation)
        {
            currState = State.Honing;
            oldHoneLocation = honeLocation;
            time = 0f;
        }
    }
}
