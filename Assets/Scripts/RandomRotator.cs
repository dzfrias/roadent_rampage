using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;

    [SerializeField] private float interval;
    [SerializeField] private Vector2 range;

    void Start()
    {
        StartCoroutine(RandomRotate());
    }

    IEnumerator RandomRotate()
    {
        while (true)
        {
            float xRot = x ? RandomInRange() : transform.rotation.eulerAngles.x;
            float yRot = y ? RandomInRange() : transform.rotation.eulerAngles.y;
            float zRot = z ? RandomInRange() : transform.rotation.eulerAngles.z;
            transform
                .DORotate(new Vector3(xRot, yRot, zRot), interval)
                .SetId(gameObject);

            yield return new WaitForSeconds(interval);
        }
    }

    float RandomInRange()
    {
        return Random.Range(range.x, range.y);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
