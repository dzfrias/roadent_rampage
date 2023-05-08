using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHover : ColorFade, IHoverable
{
    private Color original;

    public void HoverActivate(bool on)
    {
        if (!enabled) return;

        if (on)
        {
            // Check if not already mid-switch. This prevents a bug where
            // PlayerHover hasn't finished fading yet, so it sets `original` to
            // the color that it was mid-fade.
            if (DOTween.TweensById(this) == null)
            {
                original = GetColor();
            }
            FadeColor();
            return;
        }
        Switch(original);
    }
}
