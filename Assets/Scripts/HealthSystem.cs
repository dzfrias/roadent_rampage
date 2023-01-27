using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private float maxHealth;
    private float health;

    public HealthSystem(float health)
    {
        this.health = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;

        if (health < 0) { health = 0; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) { health = maxHealth; }
        if (OnHealthChanged != null) { OnHealthChanged(this, EventArgs.Empty); }
    }
}
