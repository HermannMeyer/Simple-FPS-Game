using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStationController : ShopController
{
    [SerializeField] GameObject landMinePrefab;
    [SerializeField] GameObject sentryGunPrefab;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopText.text = "Press 1 to get 4 land mines or press 2 to get 1 sentry gun for " + price.ToString() + "?";

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Check if player has sufficient funds
                if (scoreTracker.GetBalance() >= price)
                {
                    ObjectDeployer objectDeployer = other.GetComponentInChildren<ObjectDeployer>();
                    if (objectDeployer != null)
                    {
                        objectDeployer.objectPrefab = landMinePrefab;
                        objectDeployer.maxObjectCount = 4;
                        objectDeployer.Restock();

                    }
                    scoreTracker.SubtractFromBalance(price);
                }
                else
                {
                    print("Insufficient funds!");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (scoreTracker.GetBalance() >= price)
                {
                    ObjectDeployer objectDeployer = other.GetComponentInChildren<ObjectDeployer>();
                    if (objectDeployer != null)
                    {
                        objectDeployer.objectPrefab = sentryGunPrefab;
                        objectDeployer.maxObjectCount = 1;
                        objectDeployer.Restock();

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
