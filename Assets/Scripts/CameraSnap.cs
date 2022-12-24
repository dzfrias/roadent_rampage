using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSnap : MonoBehaviour
{
    [SerializeField] private PlayerTrigger detectionCollider;
    [SerializeField] private float blendTime = 0.5f;
    [SerializeField] private int setPriority = 100;
    private CinemachineBrain brain;
    private CinemachineVirtualCamera vcam;
    private int startPriority;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
        startPriority = vcam.Priority;
    }

    void Update()
    {
        if (detectionCollider.isColliding)
        {
            brain.m_DefaultBlend.m_Time = blendTime;
            vcam.Priority = setPriority;
        }
        else
        {
            vcam.Priority = startPriority;
            brain.m_DefaultBlend.m_Time = 0.5f;
        }
    }
}
