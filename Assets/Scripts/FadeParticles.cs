using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ParticleSystem))]
public class FadeParticles : MonoBehaviour
{
    [SerializeField] private Color toColor;
    [SerializeField] private float fadeTime = 0.5f;

    private ParticleSystem.MainModule particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>().main;
    }

    public void Activate()
    {
        // particles.startColor = toColor;
        DOTween.To(() => particles.startColor.color, x =>
        {
            // C#... this is just for setting the start color, as everything
            // here is a struct and is copied
            var c = particles.startColor;
            c.color = x;
            particles.startColor = c;
        }, toColor, fadeTime);
    }
}
