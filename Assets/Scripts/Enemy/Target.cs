using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 30f;
    public GameDirector gameDirector;

    private void Awake()
    {
        if (gameDirector == null)
        {
            gameDirector = GameObject.FindObjectOfType<GameDirector>();
        }
        //gameDirector.enemiesRemaining++;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        /*
        if (gameDirector != null)
        {
            gameDirector.enemiesRemaining--;
        } */
        
        Destroy(gameObject, .1f);
    }
}
