using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;

    private GameObject outlineObject;
    private Renderer outlineRenderer;

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;   
    }

    private Renderer CreateOutline(Material outlineMaterial, float scaleFactor, Color color)
    {
        outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer renderer =  outlineObject.GetComponent<Renderer>();

        renderer.material = outlineMaterial;
        renderer.material.SetColor("Color", color);
        renderer.material.SetFloat("Scale", scaleFactor);
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<Outline>().enabled = false;

        renderer.enabled = true;

        return renderer;
    }

    public void ToggleOutline()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(!outlineObject.activeSelf);
        }
    }
}
