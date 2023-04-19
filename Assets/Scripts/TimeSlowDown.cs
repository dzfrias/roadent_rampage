using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeSlowDown : MonoBehaviour
{
    [SerializeField] private float amount = 0.5f;
    [SerializeField] private float transitionTime = 0.7f;

    void Start()
    {
        enabled = false;
    }

    void OnEnable()
    {
        DOTween
            .To(() => Time.timeScale, x => Time.timeScale = x, amount, transitionTime)
            .SetEase(Ease.InQuad)
            .SetUpdate(true);
    }

    void OnDisable()
    {
        DOTween
            .To(() => Time.timeScale, x => Time.timeScale = x, 1, transitionTime)
            .SetEase(Ease.InQuad)
            .SetUpdate(true);
    }
}
