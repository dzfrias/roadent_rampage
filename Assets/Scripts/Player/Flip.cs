using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flip : MonoBehaviour
{
    [SerializeField] private float unflipTime;
    [SerializeField] private float upsideDownEpsilon = 0.01f;
    [SerializeField] private float rightSideEpsilon = 0.01f;
    private float flippedTime;
    private bool flipped;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            flipped = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            flipped = false;
            flippedTime = 0f;
        }
    }

    void Update()
    {
        if (flipped)
        {
            flippedTime += Time.deltaTime;
        }
    }

    public void Unflip()
    {
        if (flippedTime >= unflipTime)
        {
            if (transform.up.y < -1 + upsideDownEpsilon)
            {
                transform.rotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.forward);
            }
            else if (transform.up.y > 0 - rightSideEpsilon && transform.up.y < 0 + rightSideEpsilon)
            {
                transform.rotation = transform.rotation * Quaternion.AngleAxis(-90, Vector3.forward);
            }
        }
    }
}
