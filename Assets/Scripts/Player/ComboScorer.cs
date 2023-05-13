using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(IMover), typeof(Collider))]
public class ComboScorer : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> comboFinished;
    [SerializeField] private float airTimeMultiplier;
    [SerializeField] private float airTimeThreshold = 3f;
    [SerializeField] private float max = Mathf.Infinity;
    [SerializeField] private LayerMask resetMask;

    private IMover mover;

    // Combo fields
    private float airTime;

    void Start()
    {
        mover = GetComponent<IMover>();
    }

    void Update()
    {
        if (!mover.IsGrounded())
        {
            airTime += Time.deltaTime;
        }
        else
        {
            float totalScore = 0f;
            if (airTime >= airTimeThreshold)
            {
                totalScore += (airTime - airTimeThreshold) * airTimeMultiplier;
            }
            if (totalScore == 0f)
            {
                Reset();
                return;
            }
            totalScore = Mathf.Min(totalScore, max);
            comboFinished.Invoke(totalScore);
            AudioManager.instance.Play("landcombo");
            Debug.Log($"Finished combo with {totalScore}!");
            Reset();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if resetMask contains the GameObject's layer
        if (resetMask == (resetMask | (1 << collision.gameObject.layer)) && !mover.IsGrounded())
        {
            Reset();
        }
    }
    
    void Reset()
    {
        airTime = 0f;
    }
}
