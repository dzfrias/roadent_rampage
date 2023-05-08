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

    public void Accelerate(InputAction.CallbackContext context)
    {
        if (input.currentControlScheme == "TwoGamepads" && (context.control.device as Gamepad) != p1)
        {
            return;
        }
        accelerate.Invoke(context.ReadValue<float>());
    }

    public void Turn(InputAction.CallbackContext context)
    {
        if (input.currentControlScheme == "TwoGamepads" && (context.control.device as Gamepad) != p2)
        {
            return;
        }
        rotate.Invoke(context.ReadValue<float>());
    }

    public void Flip(InputAction.CallbackContext context)
    {
        flip.Invoke();
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (input.currentControlScheme == "TwoGamepads" && (context.control.device as Gamepad) != p2)
        {
            return;
        }
        click.Invoke(context.ReadValue<float>());
    }

    public void Pause(InputAction.CallbackContext context)
    {
        pause.Invoke();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (input.currentControlScheme == "TwoGamepads" && (context.control.device as Gamepad) != p1)
        {
            return;
        }
        aim.Invoke(context.ReadValue<Vector2>());
    }

    public void RearCamera(InputAction.CallbackContext context)
    {
        isReversed = !isReversed;
        reverseCamera.Invoke(isReversed);
    }
}
