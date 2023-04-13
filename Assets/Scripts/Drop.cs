using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drop : MonoBehaviour
{
    [SerializeField] private float startDist = 30f;
    [SerializeField] private float moveDuration = 1f;

    void Start()
    {
        gameObject.SetActive(false);
        transform.Translate(0f, startDist, 0f);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(transform.position.y - startDist, moveDuration).SetEase(Ease.OutBounce);
    }
}
