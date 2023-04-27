// Credits to: https://github.com/unity3d-jp/playgrownd/blob/master/Assets/Standard%20Assets/Utility/SimpleMouseRotator.cs
// for the bulk of this script.

using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] private Vector2 rotationRange = new Vector2(70, 70);
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float dampingTime = 0.2f;
    [SerializeField] private bool relative = true;
    
    private Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Outline outlinedObject;
    private Quaternion origionalRotation;

    void Start()
    {
        origionalRotation = transform.localRotation;
    }

    void Update()
    {
        transform.localRotation = origionalRotation;

        float inputH;
        float inputV;
        if (relative)
        {
            inputH = Input.GetAxis("Mouse X");
            inputV = Input.GetAxis("Mouse Y");

            if (targetAngles.y > 180)
            {
                targetAngles.y -= 360;
                followAngles.y -= 360;
            }
            if (targetAngles.x > 180)
            {
                targetAngles.x -= 360;
                followAngles.x -= 360;
            }
            if (targetAngles.y < -180)
            {
                targetAngles.y += 360;
                followAngles.y += 360;
            }
            if (targetAngles.x < -180)
            {
                targetAngles.x += 360;
                followAngles.x += 360;
            }

            targetAngles.y += inputH * rotationSpeed;
            targetAngles.x += inputV * rotationSpeed;

            targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
        }
        else
        {
            inputH = Input.mousePosition.x;
            inputV = Input.mousePosition.y;

            targetAngles.y = Mathf.Lerp(-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH / Screen.width);
            targetAngles.x = Mathf.Lerp(-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV / Screen.height);
        }

        followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);

        transform.localRotation = origionalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
        
        OutlineObject();
    }

    void OutlineObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (outlinedObject != null && hit.collider.gameObject != outlinedObject.gameObject)
            {
                outlinedObject.enabled = false;
            }

            if (hit.collider.gameObject.TryGetComponent(out Outline outlineObject))
            {
                outlinedObject = outlineObject;
                outlinedObject.enabled = true;
            }
        }
        else if (outlinedObject != null)
        {
            outlinedObject.enabled = false;
        }
    }

    public void Recoil()
    {
        followAngles += Vector3.right * 2;
    }
}
