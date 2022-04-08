using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] protected GameObject shopTextObj;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected GameObject scoreTrackerObj;
    [SerializeField] protected int price = 0;

    protected ScoreTracker scoreTracker;
    protected TextMeshProUGUI shopText;

    // Awake is called as the script instance is loaded (before Start).
    protected void Awake()
    {
        scoreTracker = scoreTrackerObj.GetComponent<ScoreTracker>();
        shopText = shopTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Activates when an object enters the Trigger attached to the game object this script is attached to.
    // If the game object is the player, then they are within range of the shop.
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenShop();
        }
    }
    
    // Activates when an object exits the Trigger attached to the game object this script is attached to.
    // If the player is outside of the trigger, then they are no longer within range of the shop.
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CloseShop();
        }
    }

    // Open the shop for the player's usage.
    protected void OpenShop()
    {
        // Disable the weapon holder and the player's ability to use weapons
        weaponHolder.SetActive(false);
        // Enable the shop UI
        shopTextObj.SetActive(true);
    }
    
    // Close the shop.
    protected void CloseShop()
    {
        // Disable the shop UI
        shopTextObj.SetActive(false);
        // Re-enable the weapon holder, so weapons can once again be used
        weaponHolder.SetActive(true);
    }
}
