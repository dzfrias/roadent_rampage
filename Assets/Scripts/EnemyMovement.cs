using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance;
    [SerializeField] private float warpOffset;
    [SerializeField, Range(0f, 1f)] private float speedDecrease;
    private NavMeshAgent agent;
    private Rigidbody targetRb;
    private Rigidbody rb;
    private float minSpeed;
    private bool isHit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
        if (isHit) { return; }

        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            agent.Warp(ClosestPoint(warpOffset));
            return;
        }

        agent.speed = Mathf.Max(targetRb.velocity.magnitude * speedDecrease, minSpeed);
        agent.destination = ClosestPoint(0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Debug.Log("<color=red>The target was reached!</color>");
        }
    }

    public IEnumerator Knockback(Vector3 hitPosition, float force)
    {
        agent.enabled = false;
        rb.isKinematic = false;
        Vector3 direction = (transform.position - hitPosition).normalized;
        // Stops enemy from getting hit upwards
        direction.y = 0;
        rb.AddForce(direction * force, ForceMode.Impulse);
        //rb.AddForce(-transform.forward * force, ForceMode.Impulse);
        isHit = true;
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
        rb.isKinematic = true;
        isHit = false;
    }
}
