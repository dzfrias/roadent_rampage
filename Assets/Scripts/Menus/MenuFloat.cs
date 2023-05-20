using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuFloat : MonoBehaviour
{
    [SerializeField] private float offset = 3f;
    [SerializeField] private float duration = 0.5f;

    void Start()
    {
        Sequence sequence = DOTween.Sequence().SetId(gameObject);
        sequence.Append(transform.DOMoveY(transform.position.y + offset, duration));
        sequence.Append(transform.DOMoveY(transform.position.y, duration));
        sequence.SetLoops(-1);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
