using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    public int numCandy = 15;
    public int numAnimals = 5;
    public int numGuards = 1;
    private Vector3[] candySpawns;
    private Vector3[] animalSpawns;
    private Vector3[] guardSpawns;
    public Vector3 center = new Vector3(-2, 40, 10);
    public int radiusMax = 50;
    public Object Candy;
    public Object Guard;
    public Object Animal;
    private GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        Parent = new GameObject();
        candySpawns = new Vector3[numCandy];
        animalSpawns = new Vector3[numAnimals];
        guardSpawns = new Vector3[numGuards];
        fillArray(candySpawns);
        fillArray(animalSpawns);
        fillArray(guardSpawns);
        SpawnType(guardSpawns, Guard);
        SpawnType(candySpawns, Candy);
        SpawnType(animalSpawns, Animal);
    }

    void fillArray(Vector3[] arr)
    {
        
        for (int i = 0; i < arr.Length; i++)
        {
            int r = UnityEngine.Random.Range(0, radiusMax);
            float theta1 = UnityEngine.Random.Range(0f, Mathf.Deg2Rad * 360);
            float theta2 = UnityEngine.Random.Range(0f, Mathf.Deg2Rad * 360);
            arr[i] = new Vector3(Mathf.Cos(theta1) * r, 0, Mathf.Sin(theta2) * r) + center;
        }
    }

    void SpawnType(Vector3[] arr, Object type)
    {
        foreach(Vector3 v in arr)
        {
            Instantiate(type, v, Quaternion.identity, Parent.transform);
        }
    }

    // Update is called once per frame
}