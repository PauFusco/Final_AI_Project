using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive_Manager : MonoBehaviour
{
    public GameObject BeePrefab;
    public int numBees = 20;
    public GameObject[] allBees;
    public Vector3 flyLimits = new Vector3(1, 1, 1);

    [Header("Bee Settings")]
    [Range(1.0f, 5.0f)]
    public float minSpeed;

    [Range(1.0f, 5.0f)]
    public float maxSpeed;

    [Range(1.0f, 10.0f)]
    public float neighbourDistance;

    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    // Use this for initialization
    private void Start()
    {
        allBees = new GameObject[numBees];
        for (int i = 0; i < numBees; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-flyLimits.x, flyLimits.x),
                                                                  Random.Range(-flyLimits.y, flyLimits.y),
                                                                  Random.Range(-flyLimits.z, flyLimits.z));
            allBees[i] = (GameObject)Instantiate(BeePrefab, pos, Quaternion.identity);
            allBees[i].GetComponent<Flocking>().Hive_Manager = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}