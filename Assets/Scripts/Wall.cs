using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHittable
{
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = new HealthSystem(10f);

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        healthSystem.Damage(1f);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) 
    {
        if (healthSystem.GetHealth() <= 0) { Destroy(this.gameObject); }
    }
}
