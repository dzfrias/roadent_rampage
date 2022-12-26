using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

enum Target
{
    Main,
    RearView,
}

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float angularFrequency;
    [SerializeField] private float dampingRatio;
    [SerializeField] private Transform target;

    private CinemachineVirtualCamera vcam;
    private Target currentTarget;
    private float velocity;
    private float rotation;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        currentTarget = Target.Main;
    }

    void Update()
    {
        if (currentTarget == Target.RearView && rotation == 0)
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        while (currentTarget == Target.RearView)
        {
            float deltaTime = Time.deltaTime;
            SpringMotion.CalcDampedSimpleHarmonicMotion(ref rotation,
                    ref velocity,
                    180f,
                    deltaTime,
                    angularFrequency,
                    dampingRatio);
            transform.rotation = target.rotation * Quaternion.AngleAxis(rotation, Vector3.up);
            yield return null;
        }
        rotation = 0;
        velocity = 0;
    }

    public void ChangeTarget()
    {
        if (currentTarget == Target.Main)
        {
            vcam.LookAt = null;
            vcam.m_Lens.Dutch = 6;
            vcam.m_Lens.FieldOfView = 90;
            currentTarget = Target.RearView;
        } else
        {
            vcam.LookAt = target;
            vcam.m_Lens.Dutch = 0;
            vcam.m_Lens.FieldOfView = 60;
            currentTarget = Target.Main;
            rotation = 0f;
            velocity = 0f;
        }
    }
}
