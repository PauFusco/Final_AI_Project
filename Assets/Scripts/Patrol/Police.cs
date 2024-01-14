using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Police : MonoBehaviour
{
    public Camera frustum;
    public LayerMask mask;
    public NavMeshAgent followerAgent;
    public NavMeshAgent ghostAgent;

    private Vector3 tempTarget;
    private Vector3 worldTarget;
    private bool following = false;
    private GameObject tofollow;

    public class PerceptionEvent
    {
        public enum senses
        { VISION };

        public enum types
        { NEW, LOST };

        public enum threatLvls
        { FRIEND, SUS, THREAT }

        public GameObject gObj;
        public senses sense;
        public types type;
        public threatLvls threatLvl;
    }

    private float timeMin = 0.3f;
    private float timeMax = 0.8f;
    private float waitTime = 0.0f;

    public float radius = 1;
    public float offset = 1;

    private NavMeshHit hat;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        following = false;
        tofollow = ghostAgent.gameObject;
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                    if (hit.collider.gameObject.CompareTag("Robber"))
                    {
                        following = true;
                        tofollow = hit.collider.gameObject;
                        break;
                    }
            }
        }

        followerAgent.destination = tofollow.transform.position;
    }
}