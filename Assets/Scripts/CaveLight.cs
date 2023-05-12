using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveLight : MonoBehaviour, IHittable
{   
    [SerializeField] private GameObject spotLight;
    [SerializeField] private float lightOnTimer = 5f;


    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        if (spotLight.activeSelf) { return; }

        StartCoroutine(ActivateLight());
    }

    IEnumerator ActivateLight()
    {
        spotLight.SetActive(true);
        yield return new WaitForSeconds(lightOnTimer);
        spotLight.SetActive(false);
    }
}
