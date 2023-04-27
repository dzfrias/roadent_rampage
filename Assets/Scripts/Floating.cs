using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floating : MonoBehaviour
{
    [SerializeField] private float moveAmount = 1f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Vector3 spawnMove;
    [SerializeField] private float spawnMoveDuration = 0.8f;

    private Vector3 start;
    private bool firstEnable;

    void Start()
    {
        float startPos = transform.position.y;
        transform.DOMoveY(startPos + moveAmount, moveDuration).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo).SetId(gameObject);
        start = transform.localPosition;
        gameObject.SetActive(false);
        transform.Translate(spawnMove, Space.World);
    }

    void OnEnable()
    {
        if (!firstEnable)
        {
            firstEnable = true;
            return;
        }
        transform.DOLocalMove(start, spawnMoveDuration).SetEase(Ease.OutElastic);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
