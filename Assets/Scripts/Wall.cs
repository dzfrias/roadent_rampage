using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringResize))]
public class Wall : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float velocityAdd = 50f;
    [SerializeField] private float health = 10f;

    private HealthSystem healthSystem;
    private SpringResize springResize;

    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        springResize = GetComponent<SpringResize>();
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        healthSystem.Damage(1f);
        GameObject p = Instantiate(particles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
        springResize.SetVelocity(velocityAdd);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) 
    {
        if (healthSystem.GetHealth() <= 0) Destroy(this.gameObject);
    }
}
