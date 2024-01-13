using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private PatrolBall ghost;

    public NavMeshAgent followerAgent;
    public NavMeshAgent ghostAgent;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        followerAgent.destination = ghostAgent.transform.position;
    }
}