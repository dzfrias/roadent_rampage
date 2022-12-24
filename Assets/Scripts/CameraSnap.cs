using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSnap : MonoBehaviour
{
    [SerializeField] private PlayerTrigger detectionCollider;
    [SerializeField] private int setPriority = 100;
    private CinemachineVirtualCamera vcam;
    private Transform player;
    private int startPriority;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player").transform;
        startPriority = vcam.Priority;
    }

    void Update()
    {
        if (detectionCollider.isColliding)
        {
            vcam.Priority = setPriority;
        }
        else
        {
            vcam.Priority = startPriority;
        }
    }
}
