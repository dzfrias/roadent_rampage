using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darken : MonoBehaviour
{
    [SerializeField] private GameObject toDarken;

    void Start()
    {
        GameManager.onDarken += Activate;
    }

    void OnDestroy()
    {
        GameManager.onDarken -= Activate;
    }

    void Activate(bool active)
    {
        toDarken.SetActive(active);
    }
}
