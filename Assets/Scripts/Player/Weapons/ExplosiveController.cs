using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    // The device's blast radius. Anything caught in here will be affected.
    [SerializeField] protected float blastRadius = 7.5f;
    // The force that will be exerted upon objects with Rigidbody
    [SerializeField] protected float explosionForce = 150f;
    // Damage that will be inflicted upon objects
    [SerializeField] protected float damage = 80f;

    // Reproduce explosion effect in Unity. Apply a force to any object with a Rigidbody and deal damage to both enemies and player.
    protected void Explode()
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

        // Remove the explosive device from the scene
        Destroy(gameObject);
    }
}
