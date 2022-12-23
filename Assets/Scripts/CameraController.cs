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
        if (target == Target.RearView)
        {
            // Make angle between -180 and 180
            float angle = player.rotation.eulerAngles.y;
            angle %= 360; 
            angle = (angle + 360) % 360;  
            if (angle > 180)  
            {
                angle -= 360;
            }

            float deltaTime = Time.deltaTime;
            SpringMotion.CalcDampedSimpleHarmonicMotion(ref rotation,
                    ref velocity,
                    angle + 180,
                    deltaTime,
                    angularFrequency,
                    dampingRatio);
            transform.rotation = Quaternion.Euler(rotation / 12, rotation, 0);
        }
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
