using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floating : MonoBehaviour
{
    [SerializeField] private float moveAmount = 1f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float spawnMoveAmount;
    [SerializeField] private float spawnMoveDuration = 0.8f;

    private float startX;
    private bool firstEnable;

    void Start()
    {
        float startPos = transform.position.y;
        transform.DOMoveY(startPos + moveAmount, moveDuration).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo).SetId(gameObject);
        startX = transform.localPosition.x;
        gameObject.SetActive(false);
        transform.Translate(new Vector3(-spawnMoveAmount, 0, 0), Space.World);
    }

    void OnEnable()
    {
        if (!firstEnable)
        {
            firstEnable = true;
            return;
        }
        transform.DOLocalMoveX(startX, spawnMoveDuration).SetEase(Ease.OutElastic);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
