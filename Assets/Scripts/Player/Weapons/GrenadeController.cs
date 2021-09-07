using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float fuzeTime = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 100f;
    public float damage = 50f;

    float countdown;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = fuzeTime;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

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

        // Remove the grenade from the scene
        Destroy(gameObject);
    }
}
