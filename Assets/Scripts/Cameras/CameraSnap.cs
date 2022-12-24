using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSnap : MonoBehaviour
{
    [SerializeField] private PlayerTrigger detectionCollider;
    [SerializeField] private int setPriority = 100;
    private CinemachineBrain brain;
    private CinemachineVirtualCamera vcam;
    private int startPriority;
    private float startBlendTime;
    private bool colliding;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
        startPriority = vcam.Priority;
        startBlendTime = brain.m_DefaultBlend.m_Time;
    }

    void Update()
    {
        if (detectionCollider.isColliding)
        {
            vcam.Priority = setPriority;
        }
        else if (!detectionCollider.isColliding)
        {
            vcam.Priority = startPriority;
        }
    }
}
