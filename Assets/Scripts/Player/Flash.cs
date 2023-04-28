using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float time;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }

    public void Activate()
    {
        light.enabled = true;
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(time);
        light.enabled = false;
    }
}
