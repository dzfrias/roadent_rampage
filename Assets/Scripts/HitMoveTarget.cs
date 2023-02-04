using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitMoveTarget : MonoBehaviour, IHittable
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 moveBy;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private Color inactiveColor;

    private bool hit;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public void Hit(Vector3 point, Vector3 direction)
    {
        if (hit) return;
        target.DOMove(target.position + moveBy, moveDuration);
        material.DOColor(inactiveColor, moveDuration * 1.5f);
        hit = true;
    }
}
