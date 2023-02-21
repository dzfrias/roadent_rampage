using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMover : MonoBehaviour
{
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
        foreach (var waypoint in waypoints)
        {
            sequence.Append(transform.DOMove(waypoint.transform.position, waypoint.moveDuration));
            sequence.Append(transform.DORotate(waypoint.transform.rotation.eulerAngles, waypoint.rotateDuration));
        }
    }
}
