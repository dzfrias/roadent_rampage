using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorFade : MonoBehaviour
{
    [SerializeField] private int materialIndex;
    [SerializeField] private Color switchTo;
    [SerializeField] private string colorProperty;
    [SerializeField] private float fadeTime = 0.5f;

    [SerializeField] private Renderer render;

    private Material mat;

    void Start()
    {
        if (render == null)
        {
            render = GetComponent<Renderer>();
        }

        mat = render.materials[materialIndex];
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
