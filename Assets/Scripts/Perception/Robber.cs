using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    public GameObject[] grannys;
    public GameObject[] trees;
    public GameObject policeman;

    public NavMeshAgent Policeman;
    private GameObject target;

    private float dist2Steal = 1f;
    private float dist2Cop = 5f;
    private Moves moves;
    private NavMeshAgent agent;

    private bool stolen = false;
    private WaitForSeconds wait = new WaitForSeconds(0.05f); // == 1/20

    private delegate IEnumerator State();

    private States state;

    private enum States
    {
        Wander,
        Seek,
        Hide
    }

    private void Start()
    {
        moves = gameObject.GetComponent<Moves>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        agent.speed = 2.6f;

        state = States.Wander;

        target = grannys[Random.Range(0, grannys.Length - 1)];

        moves.objective = target;

        StartCoroutine(switchState());
    }

    private IEnumerator switchState()
    {
        while (true)
        {
            switch (state)
            {
                case States.Wander:
                    state = IsGuarded() ? States.Wander : States.Seek;
                    yield return Wander();
                    break;

                case States.Seek:
                    state = IsGuarded() ? States.Wander : States.Seek;
                    if (CanSteal() && !stolen)
                    {
                        state = States.Hide;
                    }
                    yield return Approaching();
                    break;

                case States.Hide:
                    agent.speed = 3f;
                    yield return Hiding();
                    break;
            }
            yield return switchState();
        }
    }

    private IEnumerator Wander()
    {
        moves.Wander();
        yield return null;
    }

    private IEnumerator Approaching()
    {
        moves.Seek(target.transform.position);
        yield return null;
    }

    private IEnumerator Hiding()
    {
        moves.Hide(Policeman);
        yield return null;
    }

    private bool IsGuarded()
    {
        return (Vector3.Distance(Policeman.transform.position, target.transform.position) <= dist2Cop);
    }

    private bool CanSteal()
    {
        return (Vector3.Distance(agent.transform.position, target.transform.position) <= dist2Steal);
    }
}