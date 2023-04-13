using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMover : MonoBehaviour
{
    [SerializeField] bool loop = false;
    [SerializeField] Ease movementEase = Ease.Linear;
    [SerializeField] Ease rotationEase = Ease.Linear;
    [System.Serializable]
    public struct Waypoint
    {
        public Transform transform;
        public float moveDuration;
        public float rotateDuration;
    }

    [SerializeField] private Waypoint[] waypoints;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        if (loop) { sequence.SetLoops(-1); }
        foreach (var waypoint in waypoints)
        {
            sequence.Append(transform.DOMove(waypoint.transform.position, waypoint.moveDuration).SetEase(movementEase));
            sequence.Append(transform.DORotate(waypoint.transform.rotation.eulerAngles, waypoint.rotateDuration).SetEase(rotationEase));
        }
    }
}
