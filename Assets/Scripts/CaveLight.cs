using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CaveLight : MonoBehaviour, IHittable
{   
    [SerializeField] private GameObject spotLight;
    [SerializeField] private float lightOnTimer = 5f;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private UnityEvent outline;

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        if (spotLight.activeSelf) { return; }

        StartCoroutine(ActivateLight());
    }

    IEnumerator ActivateLight()
    {
        spotLight.SetActive(true);
        outline.Invoke();
        yield return new WaitForSeconds(lightOnTimer);
        var light = spotLight.GetComponent<Light>();
        light
            .DOIntensity(0f, transitionDuration)
            .OnComplete(() => spotLight.SetActive(false));
        outline.Invoke();
    }
}
