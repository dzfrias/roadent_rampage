using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(IMover))]
public class Flip : MonoBehaviour
{
    [SerializeField] private float showMessageTime = 1f;

    private IMover mover;
    private bool isTouching;
    private float flippedTime;
    private bool showedMessage;

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

    void Update()
    {
        if (flippedTime >= showMessageTime && !showedMessage)
        {
            GameManager.instance.BroadcastText("Press \\Flip to unflip your car!");
            showedMessage = true;
        }

        if (isTouching && !mover.IsGrounded())
        {
            flippedTime += Time.deltaTime;
        }
        else
        {
            flippedTime = 0;
            if (showedMessage)
            {
                GameManager.instance.ClearBroadcast();
                showedMessage = false;
            }
        }
    }

    public void Unflip()
    {
        if (isTouching && !mover.IsGrounded())
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
