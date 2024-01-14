using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PatrolBall : MonoBehaviour
{
    public NavMeshAgent ghostAgent;
    public NavMeshAgent followerAgent;

    public GameObject[] waypoints;
    private int patrolWP, direction;

    private void Seek(Vector3 WPpos)
    {
        ghostAgent.destination = WPpos;
    }

    private void SeekSwitch()
    {
        if (followerAgent.remainingDistance > 10) ghostAgent.isStopped = true;
        else ghostAgent.isStopped = false;
    }

    private void Patrol()
    {
        int dirNum = -1;
        switch (direction)
        {
            case 0:
                dirNum = 1;
                break;

            case 1:
                dirNum = -1;
                break;
        }
        patrolWP = (patrolWP + dirNum);

        if (patrolWP < 0) patrolWP = waypoints.Length - 1;
        if (patrolWP > waypoints.Length) patrolWP %= waypoints.Length;

        Seek(waypoints[patrolWP].transform.position);
    }

    // Start is called before the first frame update
    private void Start()
    {
        patrolWP = Random.Range(0, 4);
        direction = Random.Range(0, 2);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!ghostAgent.pathPending && ghostAgent.remainingDistance < 0.5f) Patrol();

        SeekSwitch();
    }
}