using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMover : MonoBehaviour
{
    [SerializeField] GameObject waypointParent;
    private GameObject previousWaypointParent;
    [SerializeField] bool loop = false;
    [SerializeField] Ease movementEase = Ease.Linear;
    [SerializeField] Ease rotationEase = Ease.Linear;
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

    [SerializeField] private Waypoint[] waypoints;

    private void OnValidate() {
        // Checks if waypointParent variable has been changed
        if (waypointParent != null && previousWaypointParent != waypointParent)
        {
            // Updates waypoints with children of waypointParent
            int i = 0;
            foreach (Transform childTransform in waypointParent.transform)
            {
                Waypoint waypoint = new Waypoint(childTransform, 2f, 0f);
                waypoints[i] = waypoint;
                i++;
            }
        }
        previousWaypointParent = waypointParent;
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
