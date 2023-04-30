using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class PathMover : MonoBehaviour
{
    [SerializeField] private bool loop = false;
    [SerializeField] private Ease movementEase = Ease.Linear;
    [SerializeField] private Ease rotationEase = Ease.Linear;

    [Header("Waypoints")]
    [SerializeField] private GameObject waypointParent;
    [SerializeField] private Waypoint[] waypoints;

    [System.Serializable]
    public struct Waypoint
    {
        public Transform transform;
        public float moveDuration;
        public float rotateDuration;

        public Waypoint(Transform transform, float moveDuration, float rotateDuration)
        {
            this.transform = transform;
            this.moveDuration = moveDuration;
            this.rotateDuration = rotateDuration;
        }
    }

    void OnValidate() {
        if (waypointParent == null) return;

        waypoints = waypointParent
                        .GetComponentsInChildren<Transform>()
                        // Skip because it gets waypointParent's transform
                        .Skip(1)
                        .Select(t => new Waypoint(t, 1f, 1f))
                        .ToArray();
    }

    void Start()
    {
        Sequence sequence = DOTween.Sequence().SetId(gameObject);
        if (loop) sequence.SetLoops(-1); 
        foreach (var waypoint in waypoints)
        {
            sequence.Append(transform.DOMove(waypoint.transform.position, waypoint.moveDuration).SetEase(movementEase));
            sequence.Append(transform.DORotate(waypoint.transform.rotation.eulerAngles, waypoint.rotateDuration).SetEase(rotationEase));
        }
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
