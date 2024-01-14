using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    private float radius = 1;
    private float offset = 1;

    private NavMeshHit hit;
    private Vector3 tempTarget;
    private Vector3 worldTarget;

    public NavMeshAgent agent;
    public GameObject objective;

    public GameObject[] obstacles;

    public void Wander()
    {
        tempTarget = Random.insideUnitCircle * radius;
        tempTarget += new Vector3(0, 0, offset);
        worldTarget = transform.TransformPoint(tempTarget);
        worldTarget.y = 0f;
        if (NavMesh.SamplePosition(worldTarget, out hit, 1.0f, NavMesh.AllAreas)) agent.destination = hit.position;
    }

    public void Seek(Vector3 position)
    {
        agent.destination = position;
    }

    public void Hide(NavMeshAgent hideFrom)
    {
        GameObject closestObj = null;
        float closestDist = -1;
        foreach (GameObject item in obstacles)
        {
            float temp = Vector3.Distance(hideFrom.transform.position, item.transform.position);
            if (closestDist == -1 || temp < closestDist)
            {
                closestObj = item;
                closestDist = temp;
            }
        }
        float RelDist = 1.65f / closestDist;
        Vector3 lerpPoint = Vector3.Lerp(closestObj.transform.position, hideFrom.transform.position, RelDist);

        agent.destination = closestObj.transform.position + (closestObj.transform.position - lerpPoint);
    }
}