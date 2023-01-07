using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}!");
        }
        else
        {
            Debug.Log("Hit nothing");
        }
    }
}
