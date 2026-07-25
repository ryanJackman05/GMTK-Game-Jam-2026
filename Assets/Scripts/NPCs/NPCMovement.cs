using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waypointDistance = 0.1f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        // Move towards the current waypoints
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // check if we've reached the waypoint
        if (Vector2.Distance(transform.position, target.position) <= waypointDistance)
        {
            currentWaypoint++;

            // loop back to the first waypoint
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }
}
