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
    [SerializeField] private float velocityDivide;
    [SerializeField] private float resizeMin;

    void Start()
    {
        startSize = transform.localScale;
    }

    void Update()
    {
        // Finished swelling up
        if (velocity > 0 && velocity < 0.0001)
        {
            targetSize = 0;
        }

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
        targetSize = resizeMin + (collision.relativeVelocity.magnitude / velocityDivide);
    }
}
