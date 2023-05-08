using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ColorFade : MonoBehaviour
{
    [SerializeField] private int materialIndex;
    [SerializeField] private Color switchTo;
    [SerializeField] private string colorProperty;
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private bool onStart;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().materials[materialIndex];
        if (onStart) FadeColor();
    }

    public void FadeColor()
    {
        Switch(switchTo);
    }

    public void Switch(Color color)
    {
        if (colorProperty.Length > 0)
        {
            mat.DOColor(color, colorProperty, fadeTime).SetId(this);
            return;
        }
        mat.DOColor(color, fadeTime).SetId(this);
    }

    public Color GetColor()
    {
        return mat.GetColor(colorProperty.Length == 0 ? "_Color" : colorProperty);
    }
}
