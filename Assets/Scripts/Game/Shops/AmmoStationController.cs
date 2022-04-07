using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStationController : ShopController
{
    // Activates as long as an object stays within the Trigger attached to the game object this script is attached to.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Refill ammo for " + price.ToString() + "points?";
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    // Refill the ammunition for every firearm the player possesses
                    foreach (Transform transform in weaponHolder.transform)
                    {
                        GunController gunController = transform.GetComponent<GunController>();
                        if (gunController != null)
                        {
                            print(gunController.name);
                            gunController.Refill();
                        }
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
