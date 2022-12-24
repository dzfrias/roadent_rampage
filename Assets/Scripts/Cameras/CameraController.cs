using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

enum Target
{
    Player,
    RearView,
}

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Target target;
    private Transform player;
    private float velocity;
    private float rotation;

    [SerializeField] private float angularFrequency;
    [SerializeField] private float dampingRatio;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        target = Target.Player;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target == Target.RearView && rotation == 0)
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        while (target == Target.RearView)
        {
            float deltaTime = Time.deltaTime;
            SpringMotion.CalcDampedSimpleHarmonicMotion(ref rotation,
                    ref velocity,
                    180f,
                    deltaTime,
                    angularFrequency,
                    dampingRatio);
            transform.rotation = player.rotation * Quaternion.AngleAxis(rotation, Vector3.up);
            yield return null;
        }
        rotation = 0;
        velocity = 0;
    }

    public void ChangeTarget()
    {
        if (target == Target.Player)
        {
            vcam.LookAt = null;
            vcam.m_Lens.Dutch = 6;
            vcam.m_Lens.FieldOfView = 90;
            target = Target.RearView;
        } else
        {
            vcam.LookAt = player;
            vcam.m_Lens.Dutch = 0;
            vcam.m_Lens.FieldOfView = 60;
            target = Target.Player;
            rotation = 0f;
            velocity = 0f;
        }
    }
}
