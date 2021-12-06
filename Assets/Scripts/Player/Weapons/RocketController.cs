using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : ExplosiveController
{
    [SerializeField] float moveForce = 100f;
    Camera mainCamera;

    Rigidbody rb;
    Vector3 direction;

    private void OnCollisionEnter(Collision collision)
    {
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
}
