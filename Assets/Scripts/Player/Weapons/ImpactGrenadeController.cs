using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenadeController : ExplosiveController
{
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
