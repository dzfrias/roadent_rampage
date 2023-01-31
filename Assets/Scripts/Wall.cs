using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringResize))]
public class Wall : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private GameObject destroyParticles;
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
        AudioManager.instance.Play("hit");
        healthSystem.Damage(1f);
        GameObject p = Instantiate(hitParticles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
        springResize.SetVelocity(velocityAdd);
    }

    void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) 
    {
        if (healthSystem.GetHealth() <= 0) {
            GameObject p = Instantiate(destroyParticles, transform.position, Quaternion.identity);
            p.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}
