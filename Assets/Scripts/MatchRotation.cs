using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Quaternion offset;

    void Start()
    {
        offset = transform.localRotation;
    }
    
    void Update()
    {
        transform.localRotation = target.localRotation * offset;
    }
}
