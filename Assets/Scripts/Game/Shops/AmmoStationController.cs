using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStationController : ShopController
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Refill ammo for " + price.ToString() + "?";
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
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