using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f; // Rate of attack (per second).
    [SerializeField] float damage = 10f; // Amount of damage inflicted for each attack.

    GameObject player;
    PlayerHealth playerHealth;
    bool isInRange; // Whether the player is in range of the enemy or not.
    float countdown; // Countdown to track the time between each attack.

    // Start is called before the first frame update.
    void Start()
    {
        isInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        countdown = 0f;
    }

    // Activates when an object enters the Trigger attached to the game object this script is attached to.
    // If the game object is the player, then they are within range of the enemy's attack.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    // Activates when an object exits the Trigger attached to the game object this script is attached to.
    // If the player is outside of the trigger, then they are no longer within range of the enemy's attack.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    // Update is called once per frame.
    void Update()
    {
        countdown += Time.deltaTime;

        if (countdown >= cooldown && isInRange)
        {
            countdown = 0;
            Attack();
        }
    }

    // Subtract the player's health points by the amount of damage received. 
    void Attack()
    {
        playerHealth.TakeDamage(damage);
    }
}
