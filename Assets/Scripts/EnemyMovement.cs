using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector3 warpOffset;
    [SerializeField, Range(0f, 1f)] private float speedDecrease;
    private NavMeshAgent agent;
    private Rigidbody targetRb;
    private float minSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        minSpeed = agent.speed;
        targetRb = target.gameObject.GetComponent<Rigidbody>();
        // Run every 0.2 seconds
        InvokeRepeating("SetDestination", 0f, 0.2f);
    }

    Vector3 ClosestPoint()
    {
        NavMeshHit hit;
        // Get closest position to target
        NavMesh.SamplePosition(target.position, out hit, Mathf.Infinity, NavMesh.AllAreas);
        return hit.position;
    }

    void SetDestination()
    {
        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            agent.Warp(ClosestPoint() - warpOffset);
            return;
        }

        agent.speed = Mathf.Max(targetRb.velocity.magnitude * speedDecrease, minSpeed);
        agent.destination = ClosestPoint();
    }
}
