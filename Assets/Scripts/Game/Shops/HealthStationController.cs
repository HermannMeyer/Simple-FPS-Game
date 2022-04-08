using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStationController : ShopController
{
    // Activates as long as an object stays within the Trigger attached to the game object this script is attached to.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Restore 100 units of HP for " + price.ToString() + "points?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    // Restores the player's health by 100 HPs
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.Heal(100f);
                    scoreTracker.SubtractFromBalance(price);
                }
                else
                {
                    print("Insufficient funds!");
                }
            }
        }
    }
}
