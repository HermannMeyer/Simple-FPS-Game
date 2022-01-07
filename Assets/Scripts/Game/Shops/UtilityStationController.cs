using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStationController : ShopController
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Restore 100 units of HP for " + price.ToString() + "?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    
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
