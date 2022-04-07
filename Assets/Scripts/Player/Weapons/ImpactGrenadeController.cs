using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenadeController : ExplosiveController
{
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Explode on contact to simulate a contact fuze
        Explode();
    }
}
