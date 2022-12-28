using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePushBack : CinemachineExtension
{
    [SerializeField] private float offset;
    [SerializeField] private float angularFrequency;
    [SerializeField, Range(0f, 1f)] private float dampingRatio;

    private float currentOffset;
    private float currentTarget;
    private float velocity;

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref currentOffset,
                ref velocity,
                currentTarget,
                deltaTime,
                angularFrequency,
                dampingRatio);
    }

    public void Activate()
    {
        currentTarget = offset;
    }
    
    public void Deactivate()
    {
        currentTarget = 0f;
    }

    protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        state.PositionCorrection = state.PositionCorrection - transform.forward * currentOffset;
    }
}
