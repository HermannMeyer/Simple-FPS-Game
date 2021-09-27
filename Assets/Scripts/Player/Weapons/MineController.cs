using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float blastRadius = 8f;
    public float damage = 100f;
    public float explosionForce = 100f;

    //bool isActivated = false;
    //bool hasExploded = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "Player"))
        {
            Explode();
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

        // Remove the land mine from the scene
        Destroy(gameObject);
    }
}
