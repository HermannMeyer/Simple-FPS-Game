using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    // Start is called before the first frame update.
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame.
    void Update()
    {
        // Variable to keep track of previously held weapon
        int previousWeapon = currentWeapon;

        // Mouse Scroll Wheel in forward order
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        // Mouse Scroll Wheel in reverse order 
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            currentWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            currentWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            currentWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            currentWeapon = 3;
        }
        
        // Check if the currently selected weapon is different to the previous one
        if (previousWeapon != currentWeapon)
        {
            SelectWeapon();
        }
    }

    // Choose a certain weapon to use.
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    // Add a certain weapon to the weapon holder for use by the player.
    public bool AddWeapon(GameObject weaponPrefab)
    {
        string weaponName = weaponPrefab.GetComponent<GunController>().GetWeaponName();
        foreach (Transform tf in transform)
        {
            string curWeaponName = tf.GetComponent<GunController>().GetWeaponName();
            if (curWeaponName == weaponName)
            {
                return false;
            }
        }
        GameObject weapon = Instantiate(weaponPrefab, transform);
        weapon.SetActive(false);
        return true;
    }

    // Remove a certain weapon from the holder.
    // Currently a placeholder.
    public void RemoveWeapon()
    {
        // Placeholder
    }
}
