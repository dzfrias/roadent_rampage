using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex = 0;

    private NavMeshAgent agent;
    private Rigidbody targetRb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[waypointIndex].position;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform == waypoints[waypointIndex])
        {
            waypointIndex++;
            if (waypointIndex == waypoints.Length)
            {
                Destroy(gameObject);
                return;
            }
            agent.destination = waypoints[waypointIndex].position;
        }
    }
}
