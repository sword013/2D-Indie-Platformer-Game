using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public Transform target;
    int targetIndex;
    float speed = 10f;

    public void FollowPath(Vector3[] waypoints)
    {
        targetIndex = 0;
        Vector3 currentWaypoint = waypoints[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= waypoints.Length)
                    break;
            }
            currentWaypoint = waypoints[targetIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    }
}
