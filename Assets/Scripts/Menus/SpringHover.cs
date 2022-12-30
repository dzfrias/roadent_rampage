using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpringHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float angularFrequency = 15f;
    [SerializeField, Range(0, 1)] private float dampingRatio = 0.3f;
    [SerializeField] private float distanceAdd = 5f;

    private float velocity;
    private float distance;
    private Vector3 startPos;
    private float targetAdd;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref distance,
                ref velocity,
                targetAdd,
                deltaTime,
                angularFrequency,
                dampingRatio);
        transform.position = new Vector3(startPos.x, startPos.y + distance, startPos.z);
    }

    public void OnPointerEnter(PointerEventData _)
    {
        targetAdd = distanceAdd;
    }

    public void OnPointerExit(PointerEventData _)
    {
        targetAdd = 0f;
    }
}
