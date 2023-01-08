using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private float rotateMultiplier = 0.99f;
    private Vector3 rotate;

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
    }
}
