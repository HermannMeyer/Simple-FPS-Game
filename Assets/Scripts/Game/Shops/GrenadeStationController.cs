using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeStationController : ShopController
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Restock hand grenades for " + price.ToString() + "?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
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
