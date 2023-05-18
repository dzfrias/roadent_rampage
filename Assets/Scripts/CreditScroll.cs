using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }
}
