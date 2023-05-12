using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stalagmite : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float time = 0.75f;

    public void Activate()
    {
        transform.DOMoveY(transform.position.y - distance, time);
    }
}
