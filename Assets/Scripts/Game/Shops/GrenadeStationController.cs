using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeStationController : ShopController
{
    // Activates as long as an object stays within the Trigger attached to the game object this script is attached to.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Restock hand grenades for " + price.ToString() + "points?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    // Restore the number of grenades carried to the maximum possible.
                    GrenadeThrower grenadeThrower = other.GetComponentInChildren<GrenadeThrower>();
                    if (grenadeThrower != null)
                    {
                        grenadeThrower.Restock();
                        print("Grenade restock successful!");
                    }
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
