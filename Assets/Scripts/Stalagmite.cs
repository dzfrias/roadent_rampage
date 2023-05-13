using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Stalagmite : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float time = 0.75f;
    [SerializeField] private UnityEvent onFallingHit;

    private bool falling;

    public void Activate()
    {
        falling = true;
        transform
            .DOMoveY(transform.position.y - distance, time)
            .SetEase(Ease.InQuad)
            .OnComplete(() => falling = false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (falling)
        {
            onFallingHit?.Invoke();
        }
    }
}
