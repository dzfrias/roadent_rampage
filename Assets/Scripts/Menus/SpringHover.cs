using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class SpringHover : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private Vector3 punch = new Vector3(1f, 1f, 0f);
    [SerializeField] private float punchDuration = 0.5f;
    [SerializeField] private int punchElasticity = 6;
    [SerializeField] private float punchDampening = 0.2f;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData _)
    {
        button.Select();
    }

    public void OnSelect(BaseEventData _)
    {
        DOTween.Complete(gameObject);
        transform
            .DOPunchScale(punch, punchDuration, punchElasticity, punchDampening)
            .SetId(gameObject)
            .SetUpdate(true);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
