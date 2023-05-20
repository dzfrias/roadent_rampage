using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PopIntoView : MonoBehaviour
{
    [SerializeField] private UnityEvent onComplete;
    [SerializeField] private float completeTimeOffset = 2.5f;

    [Header("Rotation")]
    [SerializeField] private float rotateDuration = 2f;

    [Header("Size")]
    [SerializeField] private float scaleDuration = 1.5f;
    [SerializeField] private Vector3 startSize = Vector3.zero;

    void Start()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 originalRotation = transform.rotation.eulerAngles;
        transform.localScale = startSize;
        transform.rotation = Quaternion.identity;

        transform.DOScale(originalScale, scaleDuration).SetEase(Ease.OutBounce);
        transform.DORotate(originalRotation, rotateDuration).SetEase(Ease.OutElastic);
        StartCoroutine(ActivateOnComplete());
    }

    IEnumerator ActivateOnComplete()
    {
        float waitTime = Mathf.Max(scaleDuration, rotateDuration) + completeTimeOffset;
        yield return new WaitForSeconds(waitTime);
        onComplete?.Invoke();
    }

    public void Fade(float fade)
    {
        Image img = GetComponent<Image>();
        img.DOFade(fade, 1f);
    }
}
