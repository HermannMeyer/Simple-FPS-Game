using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : ExplosiveController
{
    [SerializeField] float moveForce = 100f;
    Camera mainCamera;

    Rigidbody rb;
    Vector3 direction;

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Explode on contact to simulate a contact fuze
        Explode();
    }

    // Start is called before the first frame update.
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        direction = mainCamera.transform.forward;
    }

    // FixedUpdate has the frequency of the physics system and is called every fixed frame-rate frame.
    void FixedUpdate()
    {
        // Apply a constant force to the projectile so that it maintains its forward movement
        rb.AddForce(moveForce * direction);
    }
}
