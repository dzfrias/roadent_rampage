using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inflate : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool onStart;
    [SerializeField] private bool lockPosition = true;

    void Start()
    {
        if (!onStart)
        {
            return;
        }
        ActivateInflate();
    }

    public void ActivateInflate()
    {
        Vector3 endScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(endScale, duration).SetEase(Ease.OutQuad).SetId(gameObject);
        if (lockPosition) StartCoroutine(LockPosition());
    }

    IEnumerator LockPosition()
    {
        Vector3 startPos = transform.position;
        float d = duration;
        while (d > 0)
        {
            transform.position = startPos;
            d -= Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
