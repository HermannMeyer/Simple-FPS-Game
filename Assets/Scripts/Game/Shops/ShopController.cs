using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] protected GameObject shopPanel;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected GameObject scoreTrackerObj;

    protected ScoreTracker scoreTracker;

    protected void Awake()
    {
        scoreTracker = scoreTrackerObj.GetComponent<ScoreTracker>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenShop();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CloseShop();
        }
    }

    protected void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        weaponHolder.SetActive(false);
        shopPanel.SetActive(true);
    }

    protected void CloseShop()
    {
        shopPanel.SetActive(false);
        weaponHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
