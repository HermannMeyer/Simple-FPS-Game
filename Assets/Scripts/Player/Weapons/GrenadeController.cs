using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : ExplosiveController
{
    // Time before the grenade explodes
    public float fuzeTime = 3f;

    // Current time of the fuze
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
}
