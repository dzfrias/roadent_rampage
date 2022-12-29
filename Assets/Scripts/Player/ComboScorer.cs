using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ComboScorer : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> comboFinished;
    [SerializeField] private float airTimeMultiplier;
    [SerializeField] private float airTimeThreshold = 3f;
    [SerializeField] private GameObject moverObject;

    private IMover mover;
    private bool previousOnGround;

    // Combo fields
    private float airTime;

    void Start()
    {
        mover = moverObject.GetComponent<IMover>();
    }

    void Update()
    {
        if (!mover.IsGrounded())
        {
            airTime += Time.deltaTime;
        }
        else if (previousOnGround != mover.IsGrounded() && mover.IsGrounded())
        {
            float totalScore = 0f;
            if (airTime >= airTimeThreshold)
            {
                totalScore += airTime * airTimeMultiplier;
            }
            if (totalScore == 0f)
            {
                return;
            }
            comboFinished.Invoke(totalScore);
            Debug.Log($"Finished combo with {totalScore}!");
            Reset();
        }
        else
        {
            airTime = 0f;
        }
        previousOnGround = mover.IsGrounded();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 && !mover.IsGrounded())
        {
            Reset();
        }
    }
    
    void Reset()
    {
        airTime = 0f;
    }
}
