using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStationController : ShopController
{
    [SerializeField] GameObject weaponPrefab;

    string weaponName;
    WeaponSwitching weaponSwitching;

    private void Start()
    {
        GunController gunController = weaponPrefab.GetComponent<GunController>();
        weaponName = gunController.GetWeaponName();
        weaponSwitching = weaponHolder.GetComponent<WeaponSwitching>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Purchase " + weaponName + " for " + price.ToString() + "?";

            if (Input.GetKeyDown(KeyCode.F))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
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
