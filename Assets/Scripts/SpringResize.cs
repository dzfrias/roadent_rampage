using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpringResize : MonoBehaviour
{
    [Header("Spring Motion")]
    [SerializeField] private float angularFrequency = 10f;
    [SerializeField, Range(0f, 1f)] private float dampingRatio = 0.5f;

    [SerializeField] private bool onCollide;
    [SerializeField] private float minResize;

    private float targetSize;
    private float velocity;
    private float size;
    private Vector3 startSize;

    void Start()
    {
        startSize = transform.localScale;
        if (!onCollide && minResize > 0)
        {
            Debug.LogWarning("minResize set when onCollide is `false`");
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        SpringMotion.CalcDampedSimpleHarmonicMotion(ref size,
                ref velocity,
                targetSize,
                deltaTime,
                angularFrequency,
                dampingRatio);
        float newX = Mathf.Clamp(startSize.x + size, 0.5f, Mathf.Infinity);
        float newY = Mathf.Clamp(startSize.y + size, 0.5f, Mathf.Infinity);
        float newZ = Mathf.Clamp(startSize.z + size, 0.5f, Mathf.Infinity);
        transform.localScale = new Vector3(newX, newY, newZ);
    }

    public void SetVelocity(float velocityParam)
    {
        velocity = velocityParam;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!onCollide) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            velocity = minResize + collision.relativeVelocity.magnitude;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SpringResize))]
public class SpringResize_Editor : Editor
{
    private SerializedProperty onCollide;
    private SerializedProperty minResize;

    void OnEnable()
    {
        onCollide = serializedObject.FindProperty("onCollide");
        minResize = serializedObject.FindProperty("minResize");
    }

    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject, "onCollide", "minResize");
        serializedObject.Update();

        EditorGUILayout.PropertyField(onCollide);

        if (onCollide.boolValue)
        {
            EditorGUILayout.PropertyField(minResize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
