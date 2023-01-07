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
    [SerializeField] private UnityEvent flip;
    [SerializeField] private UnityEvent click;
    [SerializeField] private UnityEvent cameraChange;

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

    void OnCameraChange()
    {
        cameraChange.Invoke();
    }

    void OnClick()
    {
        click.Invoke();
    }
}
