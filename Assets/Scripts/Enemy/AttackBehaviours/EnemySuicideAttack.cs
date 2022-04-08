using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuicideAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float blastRadius = 6f;
    [SerializeField] float damage = 75f;
    [SerializeField] float explosionForce = 100f;
    [SerializeField] float timeToExplode = 1.5f;

    float countdown;
    bool isInRange = false; // Whether the player is in range of the enemy or not.
    bool hasExploded = false; // Whether the enemy has exploded or not.
    GameObject player;
    
    // Start is called before the first frame update.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        countdown = timeToExplode;
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

    // Update is called once per frame.
    void Update()
    {
        if (isInRange)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    // Reproduce explosion effect in Unity. Apply a force to any object with a Rigidbody and deal damage to both enemies and player.
    void Explode()
    {
        // Get all nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
        {
            // Apply explosion force to rigidbody if applicable 
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            // Apply damage to targets if applicable
            Target target = collider.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Apply damage to player if applicable
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Remove the enemy from the scene
        Destroy(gameObject);
    }
}
