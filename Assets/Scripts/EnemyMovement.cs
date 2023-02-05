using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IHittable
{
    [Header("Tracking")]
    [SerializeField] private float maxDistance;
    [SerializeField] private float warpOffset;
    [SerializeField, Range(0f, 1f)] private float speedDecrease;

    [Header("Hit Logic")]
    [SerializeField] private float hitForce = 10f;
    [SerializeField] private float hitDuration = 0.5f;
    [SerializeField] private float hitStun = 0.5f;

    private Transform target;
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
        target = GameManager.instance.player.transform;
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
        if (isHit) return; 

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

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        StartCoroutine("KnockbackTime");
        rb.AddForce(-direction * hitForce, ForceMode.Impulse);
    }

    IEnumerator KnockbackTime()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        isHit = true;
        yield return new WaitForSeconds(hitDuration);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(hitStun);
        agent.enabled = true;
        rb.isKinematic = true;
        isHit = false;
    }
}
