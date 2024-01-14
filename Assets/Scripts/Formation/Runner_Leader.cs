using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Runner_Leader : MonoBehaviour
{
    public int runner;
    public GameObject runnerPrefab;
    public GameObject ghost;

    private void Start()
    {
        int front = 2 * runner / 3;
        int rear = runner - front;
        createRow(front, -2f, runnerPrefab);
        createRow(rear, -4f, runnerPrefab);
    }

    private void createRow(int num, float z, GameObject pf)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i)
        {
            Vector3 position = ghost.transform.TransformPoint(new Vector3(pos, 0f, z));
            GameObject temp = (GameObject)Instantiate(pf, position, ghost.transform.rotation);
            temp.AddComponent<Runner>();
            temp.GetComponent<Runner>().pos = new Vector3(pos, 0, z);
            temp.GetComponent<Runner>().target = ghost;
            pos += 2f;
        }
    }
}