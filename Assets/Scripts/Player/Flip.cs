using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(IMover))]
public class Flip : MonoBehaviour
{
    private IMover mover;
    private bool isTouching;

    void Start()
    {
        mover = GetComponent<IMover>();
    }

    void OnCollisionEnter(Collision _)
    {
        isTouching = true;
    }

    void OnCollisionExit(Collision _)
    {
        isTouching = false;
    }

    public void Unflip()
    {
        if (isTouching && !mover.IsGrounded())
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
