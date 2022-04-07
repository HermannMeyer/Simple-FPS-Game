using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStationController : ShopController
{
    [SerializeField] GameObject weaponPrefab;

    string weaponName;
    WeaponSwitching weaponSwitching;

    // Start is called before the first frame update.
    private void Start()
    {
        GunController gunController = weaponPrefab.GetComponent<GunController>();
        weaponName = gunController.GetWeaponName();
        weaponSwitching = weaponHolder.GetComponent<WeaponSwitching>();
    }

    // Activates as long as an object stays within the Trigger attached to the game object this script is attached to.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Purchase " + weaponName + " for " + price.ToString() + "points?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    // Add weapon to the weapon holder for the player's use
                    if (weaponSwitching.AddWeapon(weaponPrefab))
                    {
                        scoreTracker.SubtractFromBalance(price);
                    }
                }
                else
                {
                    print("Insufficient funds!");
                }
            }
        }
    }
}
