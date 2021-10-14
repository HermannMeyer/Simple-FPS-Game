using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float blastRadius = 7.5f;
    public float explosionForce = 150f;
    public float damage = 80f;
    public float moveForce = 100f;
    Camera mainCamera;

    bool hasExploded = false;
    Rigidbody rb;
    Vector3 direction;

    private void OnCollisionEnter(Collision collision)
    {
        hasExploded = true;
        Explode();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        direction = mainCamera.transform.forward;
    }

    void FixedUpdate()
    {
        rb.AddForce(moveForce * direction);
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

        // Remove the rocket from the scene
        Destroy(gameObject);
    }
}
