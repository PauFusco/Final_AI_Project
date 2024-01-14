using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public Hive_Manager Hive_Manager;
    private float speed;
    private float timeMin = 0.3f;
    private float timeMax = 0.8f;
    private float waitTime = 0.0f;

    private Vector3 direction;

    private void Start()
    {
        speed = Random.Range(Hive_Manager.minSpeed,
                                Hive_Manager.maxSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);

        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            ApplyRules();
            waitTime = Random.Range(timeMin, timeMax);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      Hive_Manager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    private void ApplyRules()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in Hive_Manager.allBees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= Hive_Manager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;

        Vector3 align = Vector3.zero;
        num = 0;
        foreach (GameObject go in Hive_Manager.allBees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= Hive_Manager.neighbourDistance)
                {
                    align += go.GetComponent<Flocking>().direction;
                    num++;
                }
            }
        }

        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, Hive_Manager.minSpeed, Hive_Manager.maxSpeed);
        }

        Vector3 separation = Vector3.zero;

        foreach (GameObject go in Hive_Manager.allBees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= Hive_Manager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) /
                                  (distance * distance);
            }
        }

        direction = (cohesion + align + separation).normalized * speed;
        if (direction == Vector3.zero)
        {
            direction.x = 1;
            direction.y = 1;
            direction.z = 1;
        }
    }
}