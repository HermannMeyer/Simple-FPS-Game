using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject weaponHolder;

    GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            OpenShop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        weaponHolder.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        weaponHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
