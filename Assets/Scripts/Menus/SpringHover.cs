using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class SpringHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData _)
    {
        button.Select();
    }

    public void OnPointerExit(PointerEventData _)
    {
        var current = EventSystem.current;
        if (current == null) return;

        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            return;
        }
        // Deselect
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSelect(BaseEventData _)
    {
        DOTween.Complete(gameObject);
        transform.DOPunchScale(new Vector3(1f, 1f, 0f), 0.5f, 6, 0.2f).SetId(gameObject);
    }

    void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
