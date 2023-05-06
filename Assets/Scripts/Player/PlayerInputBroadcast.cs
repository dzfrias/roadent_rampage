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
    [SerializeField] private UnityEvent<Vector2> aim;
    [SerializeField] private UnityEvent flip;
    [SerializeField] private UnityEvent pause;
    [SerializeField] private UnityEvent<bool> reverseCamera;

    private Gamepad p1;
    private Gamepad p2;
    private PlayerInput input;
    private bool isReversed;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        if (input.currentControlScheme == "TwoGamepads")
        {
            p1 = Gamepad.all[0];
            p2 = Gamepad.all[1];
        }
    }

    void OnAccelerate(InputValue value)
    {
        if (input.currentControlScheme == "TwoGamepads" && Gamepad.current != p1)
        {
            return;
        }
        accelerate.Invoke(value.Get<float>());
    }

    void OnTurn(InputValue value)
    {
        if (input.currentControlScheme == "TwoGamepads" && Gamepad.current != p2)
        {
            return;
        }
        rotate.Invoke(value.Get<float>());
    }

    void OnFlip(InputValue value)
    {
        flip.Invoke();
    }

    void OnClick(InputValue value)
    {
        if (input.currentControlScheme == "TwoGamepads" && Gamepad.current != p2)
        {
            return;
        }
        click.Invoke(value.Get<float>());
    }

    void OnPause(InputValue value)
    {
        pause.Invoke();
    }

    void OnAim(InputValue value)
    {
        if (input.currentControlScheme == "TwoGamepads" && Gamepad.current != p1)
        {
            return;
        }
        aim.Invoke(value.Get<Vector2>());
    }

    void OnRearCamera(InputValue value)
    {
        isReversed = !isReversed;
        reverseCamera.Invoke(isReversed);
    }
}
