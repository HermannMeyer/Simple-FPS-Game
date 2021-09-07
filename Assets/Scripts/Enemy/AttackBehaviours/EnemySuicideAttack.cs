using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuicideAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float blastRadius = 6f;
    public float damage = 75f;
    public float explosionForce = 100f;
    public float timeToExplode = 1.5f;

    float countdown;
    bool isInRange = false;
    bool hasExploded = false;
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        countdown = timeToExplode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = true;
        }    
    }

    // Update is called once per frame
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

    void Explode()
    {
        // TODO - Add explosion effect

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
