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

    protected void Awake()
    {
        scoreTracker = scoreTrackerObj.GetComponent<ScoreTracker>();
        shopText = shopTextObj.GetComponent<TextMeshProUGUI>();
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
        weaponHolder.SetActive(false);
        shopTextObj.SetActive(true);
    }

    protected void CloseShop()
    {
        shopTextObj.SetActive(false);
        weaponHolder.SetActive(true);
    }
}
