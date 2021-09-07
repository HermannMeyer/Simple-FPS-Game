using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float cooldown = 0.5f; // Rate of attack (per second)
    public float damage = 10f;

    GameObject player;
    PlayerHealth playerHealth;
    bool isInRange = false;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        countdown = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;

        if (countdown >= cooldown && isInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        countdown = 0;

        playerHealth.TakeDamage(damage);
    }
}
