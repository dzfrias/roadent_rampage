using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringResize : MonoBehaviour
{
    [SerializeField] private float angularFrequency = 10f;
    [SerializeField, Range(0f, 1f)] private float dampingRatio = 0.5f;
    [SerializeField] private bool onCollide;
    [SerializeField] private float hitStrength = 1f;

    private float targetSize;
    private float velocity;
    private float size;
    private Vector3 startSize;

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
        float newX = Mathf.Clamp(startSize.x + size, 0.5f, Mathf.Infinity);
        float newY = Mathf.Clamp(startSize.y + size, 0.5f, Mathf.Infinity);
        float newZ = Mathf.Clamp(startSize.z + size, 0.5f, Mathf.Infinity);
        transform.localScale = new Vector3(newX, newY, newZ);
    }

    public void SetVelocity(float velocityParam)
    {
        velocity = velocityParam * hitStrength;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!onCollide) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            velocity = collision.relativeVelocity.magnitude * hitStrength;
        }
    }
}
