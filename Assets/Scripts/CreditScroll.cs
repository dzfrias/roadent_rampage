using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float completedAfter = 100f;
    [SerializeField] private UnityEvent onComplete;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (rectTransform.position.y > completedAfter)
        {
            onComplete?.Invoke();
            enabled = false;
        }
        rectTransform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }
}
