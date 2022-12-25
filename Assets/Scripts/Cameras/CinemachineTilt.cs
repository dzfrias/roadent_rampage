using UnityEngine;
using Cinemachine;

public enum CinemachineTiltDirection
{
    Up = 1,
    Rest = 0,
    Down = -1,
}

public class CinemachineTilt : CinemachineExtension
{
    [SerializeField] private float maxTilt = 5f;
    [SerializeField] private float angularFrequency;
    [SerializeField]
    [Range(0f, 1f)]
    private float dampingRatio;

    private float rotation;
    private float velocity;
    private CinemachineTiltDirection tilt;

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref rotation,
                ref velocity,
                maxTilt * -(int)tilt,
                deltaTime,
                angularFrequency,
                dampingRatio);
    }

    public void SetTilt(CinemachineTiltDirection t)
    {
        tilt = t;
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        state.OrientationCorrection = Quaternion.AngleAxis(rotation, Vector3.right);
    }
}
