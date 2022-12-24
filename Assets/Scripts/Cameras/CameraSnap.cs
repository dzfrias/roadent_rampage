using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSnap : MonoBehaviour
{
    [SerializeField] private PlayerTrigger detectionCollider;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (detectionCollider.isColliding)
        {
            vcam.enabled = true;
        }
        else if (!detectionCollider.isColliding)
        {
            vcam.enabled = false;
        }
    }
}
