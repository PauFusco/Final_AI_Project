using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    public Vector3 pos;
    public Quaternion rot;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.TransformPoint(pos);
    }

    private void Update()
    {
        agent.destination = target.transform.TransformPoint(pos);
    }
}