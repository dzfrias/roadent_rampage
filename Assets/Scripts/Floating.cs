using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floating : MonoBehaviour
{
    [SerializeField] private float moveAmount = 1f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float spawnMoveAmount;

    void Start()
    {
        float startPos = transform.position.y;
        transform.DOMoveY(startPos + moveAmount, moveDuration).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);
    }

    void OnEnable()
    {
        transform.DOMoveX(transform.position.x + spawnMoveAmount, 0.8f).SetEase(Ease.OutBounce);
    }
}
