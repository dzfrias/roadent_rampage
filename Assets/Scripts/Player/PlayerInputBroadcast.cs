using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputBroadcast : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> accelerate;
    [SerializeField] private UnityEvent<float> rotate;
    [SerializeField] private UnityEvent<float> click;
    [SerializeField] private UnityEvent flip;

    void OnAccelerate(InputValue value)
    {
        accelerate.Invoke(value.Get<float>());
    }

    void OnTurn(InputValue value)
    {
        rotate.Invoke(value.Get<float>());
    }

    void OnFlip(InputValue value)
    {
        flip.Invoke();
    }

    void OnClick(InputValue value)
    {
        click.Invoke(value.Get<float>());
    }
}
