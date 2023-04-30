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
        DOTween
            .To(() => Time.timeScale, x => Time.timeScale = x, amount, transitionTime)
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .SetId(gameObject);
    }

    void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return;
        DOTween
            .To(() => Time.timeScale, x => Time.timeScale = x, 1, transitionTime)
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .SetId(gameObject);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
