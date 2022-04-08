using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : ExplosiveController
{
    // Activates when an object enters the Trigger attached to the game object this script is attached to.
    // If the game object is the player or an enemy, then the mine is armed.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            // The mine explodes after arming.
            Explode();
        }
    }
}
