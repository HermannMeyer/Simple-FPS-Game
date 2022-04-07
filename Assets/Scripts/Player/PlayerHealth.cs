using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f; // Maximum amount of health allowed to have.
    float health = 100f; // Current amount of health.
    public bool hasDied { get; private set; } = false;

    // Return the current amount of health of the player.
    public float GetHealth()
    {
        return health;
    }

    // Subtract the player's current health by the amount of damage inflicted upon them.
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        // If the player's health reaches zero, they are considered to be dead.
        if (health <= 0)
        {
            Die();
        }
    }

    // Restore the player's health by a certain amount indicated by the variable healAmount.
    public void Heal(float healAmount)
    {
        health += healAmount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    // Set the float hasDied to true, which triggers game events affected by the player's expiration.
    void Die()
    {
        hasDied = true;
    }
}
