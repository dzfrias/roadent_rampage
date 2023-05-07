using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ColorFade : MonoBehaviour
{
    [SerializeField] private Color switchTo;
    [SerializeField] private string colorProperty;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private bool onStart;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if (onStart) FadeColor();
    }

    public void FadeColor()
    {
        if (colorProperty.Length > 0)
        {
            mat.DOColor(switchTo, colorProperty, fadeTime);
            return;
        }
        mat.DOColor(switchTo, fadeTime);
    }
}
