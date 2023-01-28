using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringResize : MonoBehaviour
{
    [Header("Spring Motion")]
    [SerializeField] private float angularFrequency;
    [SerializeField] private float dampingRatio;

    [SerializeField] private bool onCollide;
    // TODO: Make more sophisticated? Put dependency on onCollide in inspector
    [SerializeField] private float minResize;

    private float targetSize;
    private float velocity;
    private float size;
    private Vector3 startSize;

    void Start()
    {
        startSize = transform.localScale;
        if (!onCollide && minResize > 0)
        {
            Debug.LogWarning("minResize set when onCollide is `false`");
        }
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
        float newX = Mathf.Clamp(startSize.x + size, 0, Mathf.Infinity);
        float newY = Mathf.Clamp(startSize.y + size, 0, Mathf.Infinity);
        float newZ = Mathf.Clamp(startSize.z + size, 0, Mathf.Infinity);
        transform.localScale = new Vector3(newX, newY, newZ);
    }

    public void SetVelocity(float velocityParam)
    {
        velocity = velocityParam;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!onCollide) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            velocity = minResize + collision.relativeVelocity.magnitude;
        }
    }
}
