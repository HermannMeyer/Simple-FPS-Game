using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : ExplosiveController
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "Player"))
        {
            Explode();
        }
    }
}
