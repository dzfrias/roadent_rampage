using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [System.Serializable]
    public struct DegreeRange
    {
        [Range(-180, 0)] public float lowerBound;
        [Range(0, 180)] public float upperBound;
    }

    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private float rotateMultiplier = 0.99f;
    [SerializeField] private DegreeRange yRotateRange;
    [SerializeField] private DegreeRange xRotateRange;
    private Vector3 rotate;
    private Outline outlinedObject;

    public static Vector3 GetSignedEulerAngles(Vector3 angles)
    {
        Vector3 signedAngles = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            signedAngles[i] = (angles[i] + 180f) % 360f - 180f;
        }
        return signedAngles;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse Y") == 0 && Input.GetAxis("Mouse X") == 0)
        {
            rotate *= rotateMultiplier;
        }
        else
        {
            rotate = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotateSpeed;
        }
        transform.Rotate(rotate * Time.deltaTime);
        Vector3 rotation = GetSignedEulerAngles(transform.localEulerAngles);
        transform.localEulerAngles = new Vector3(
                Mathf.Clamp(rotation.x, xRotateRange.lowerBound, xRotateRange.upperBound),
                Mathf.Clamp(rotation.y, yRotateRange.lowerBound, yRotateRange.upperBound),
                rotation.z);
        
        OutlineObject();
    }

    void OutlineObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}!");
            if (hit.collider.gameObject.TryGetComponent(out Outline outlineObject))
            {
                outlinedObject = outlineObject;
                outlinedObject.enabled = true;
            }

            if (outlinedObject != null && hit.collider.gameObject != outlinedObject.gameObject)
            {
                outlinedObject.enabled = false;
            }
        }
        else if (outlinedObject != null)
        {
            outlinedObject.enabled = false;
        }
    }
}
