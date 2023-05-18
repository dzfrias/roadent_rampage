using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeSlowDown : MonoBehaviour
{
    [SerializeField] private float amount = 0.5f;
    [SerializeField] private float transitionTime = 0.7f;
    [SerializeField] private float duration = 2f;

    IEnumerator Start()
    {
        DOTween
            .To(() => Time.timeScale, x => Time.timeScale = x, amount, transitionTime)
            .SetEase(Ease.InQuad)
            .SetUpdate(true)
            .SetId(gameObject);
        yield return new WaitForSeconds(duration);
        if (!gameObject.scene.isLoaded) yield break;
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
