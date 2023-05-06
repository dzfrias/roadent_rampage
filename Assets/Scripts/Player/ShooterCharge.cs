using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ShooterCharge : MonoBehaviour
{
    [SerializeField] private Image fill;

    [Header("Bounce")]
    [SerializeField] private float angularFrequency = 10f;
    [SerializeField, Range(0f, 1f)] private float dampingRatio = 0.5f;

    private Slider slider;
    private float startY;
    private RectTransform rt;
    private float velocity;
    private float pos;

    void Start()
    {
        slider = GetComponent<Slider>();
        rt = GetComponent<RectTransform>();
        startY = rt.anchoredPosition.y;
    }

    public void SetCharge(float charge)
    {
        slider.value = charge;
    }
    
    public void Overcharge(bool overcharge)
    {
        fill.color = overcharge ? Color.red : Color.white;
    }

    public void Bounce()
    {
        velocity = 100f;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref pos,
                ref velocity,
                0f,
                deltaTime,
                angularFrequency,
                dampingRatio);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, startY + pos);

        if (slider.value == 100f)
        {
            fill.color = Color.red;
        }
    }
}
