using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCollision : MonoBehaviour
{
    private float targetSize;
    private float velocity;
    private float size;
    private Vector3 startSize;

    [Header("Spring Motion")]
    [SerializeField] private float angularFrequency;
    [SerializeField] private float dampingRatio;

    [Header("Velocity Resizing")]
    [SerializeField] private float resizeMin;
    [SerializeField] private float velocityMin;

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref size,
                ref velocity,
                targetSize,
                deltaTime,
                angularFrequency,
                dampingRatio);
        transform.localScale = new Vector3(startSize.x + size, startSize.y + size, startSize.z + size);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.magnitude < velocityMin)
            {
                return;
            }
            velocity = resizeMin + collision.relativeVelocity.magnitude;
        }
    }
}
