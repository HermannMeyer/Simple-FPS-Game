using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public bool hasDied { get; private set; } = false;

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        hasDied = true;
    }
}
