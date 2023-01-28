using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringCollision))]
public class Wall : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float velocityAdd = 50f;

    private HealthSystem healthSystem;
    private SpringCollision springCollision;

    void Start()
    {
        healthSystem = new HealthSystem(10f);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        springCollision = GetComponent<SpringCollision>();
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        healthSystem.Damage(1f);
        GameObject p = Instantiate(particles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
        springCollision.SetVelocity(velocityAdd);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) 
    {
        if (healthSystem.GetHealth() <= 0) Destroy(this.gameObject);
    }
}
