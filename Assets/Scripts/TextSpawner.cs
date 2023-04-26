using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float duration = 1.8f;

    private GameObject spawned;
    private GameObject textGameObject;
    private GameObject canvasGameObject;

    void Start()
    {
        textGameObject = text.gameObject;
        canvasGameObject = canvas.gameObject;
    }

    public void Spawn(float text)
    {
        spawned = Instantiate(textGameObject, canvasGameObject.transform);
        spawned.GetComponent<TMP_Text>().text = text.ToString();
        Destroy(spawned, duration);
    }

    public void Spawn(string text)
    {
        spawned = Instantiate(textGameObject, canvasGameObject.transform);
        spawned.GetComponent<TMP_Text>().text = text;
        Destroy(spawned, duration);
    }
}
