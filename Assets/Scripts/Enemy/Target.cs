using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int targetScore = 0;
    [SerializeField] float health = 30f;
    ScoreTracker scoreTracker;
    GameDirector gameDirector;

    private void Awake()
    {
        if (gameDirector == null)
        {
            gameDirector = GameObject.FindObjectOfType<GameDirector>();
            scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    void Die()
    {
        // Add the target's score value to the total score
        scoreTracker.AddToScore(targetScore);
        Destroy(gameObject, .1f);
    }
}
