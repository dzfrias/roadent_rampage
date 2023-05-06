using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Tracking")]
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance;
    [SerializeField] private float warpOffset;
    [SerializeField, Range(0f, 1f)] private float speedDecrease;
    [SerializeField] private UnityEvent onTargetReached;

    private NavMeshAgent agent;
    private Rigidbody targetRb;
    private float minSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        minSpeed = agent.speed;
        targetRb = target.gameObject.GetComponent<Rigidbody>();
    }

    Vector3 ClosestPoint(float offset)
    {
        NavMeshHit hit;
        // Get closest position to target
        NavMesh.SamplePosition(target.position - target.forward * offset, out hit, Mathf.Infinity, NavMesh.AllAreas);
        return hit.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            agent.Warp(ClosestPoint(warpOffset));
            return;
        }

        agent.speed = Mathf.Max(targetRb.velocity.magnitude * speedDecrease, minSpeed);
        agent.destination = target.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Debug.Log("<color=red>The target was reached!</color>");
            onTargetReached?.Invoke();
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
