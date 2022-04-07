using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int targetScore = 0;
    [SerializeField] float health = 30f;
    ScoreTracker scoreTracker;
    GameDirector gameDirector;

    // Awake is called as the script instance is loaded (before Start).
    private void Awake()
    {
        if (gameDirector == null)
        {
            gameDirector = GameObject.FindObjectOfType<GameDirector>();
            scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();
        }
    }

    // Take damage by subtracting the current health points by the amount of damage received. If health falls below 0, the target dies.
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    // Return the current amount of health.
    public float GetHealth()
    {
        return health;
    }

    // Called when the target is about to die. Add points to the player's balance, and destroy the target's game object.
    void Die()
    {
        // Add the target's score value to the total score
        scoreTracker.AddToBalance(targetScore);
        Destroy(gameObject, .1f);
    }
}
