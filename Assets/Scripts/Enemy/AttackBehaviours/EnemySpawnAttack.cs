using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnAttack : MonoBehaviour
{
    [SerializeField] float cooldown = 1f; // Rate of attack (per second)
    [SerializeField] float damage = 10f; // Amount of damage inflicted for each attack.
    [SerializeField] GameObject spawnlingPrefab; // Prefab of the Spawnling.
    [SerializeField] Target target;
    [SerializeField] int numberOfSpawnlings = 4; // Number of Spawnlings spawned after destruction of the carrier.

    GameObject player;
    PlayerHealth playerHealth;
    bool isInRange; // Whether the player is in range of the enemy or not.
    bool isDead; // Whether the enemy this script is attached to is dead or not.
    float countdown;

    // Start is called before the first frame update.
    void Start()
    {
        isInRange = false;
        isDead = false;
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
            Attack();
        }

        if (target.GetHealth() <= 0 && !isDead)
        {
            Spawn();
            isDead = true;
        }
    }

    // Subtract the player's health points by the amount of damage received. 
    void Attack()
    {
        countdown = 0;

        playerHealth.TakeDamage(damage);
    }
    
    // Spawns enemies around the location of the neutralized carrier.
    void Spawn()
    {
        // Contains the spawn locations where enemies will be spawned.
        Vector3[] spawnPositions = { transform.position + Vector3.forward, transform.position + Vector3.back,
                                    transform.position + Vector3.left, transform.position + Vector3.right };
        
        // Pick position for each enemy to be spawned.
        for (int i = 0; i < numberOfSpawnlings; i++)
        {
            int positionIndex = i % numberOfSpawnlings;
            Instantiate(spawnlingPrefab, spawnPositions[positionIndex], transform.rotation);

        }
    }
}
