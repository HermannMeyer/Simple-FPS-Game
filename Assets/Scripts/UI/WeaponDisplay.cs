using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    // public GameObject weaponHolder;
    public Text weaponText;
    public Transform weaponHolder;

    // Update is called once per frame
    void Update()
    {
        GetCurrentWeapon();
    }

    void GetCurrentWeapon()
    {
        foreach (Transform weapon in weaponHolder)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                GunController gunController = weapon.GetComponent<GunController>();
                int ammoInMag = gunController.GetAmmoCountInMag();
                int totalAmmo = gunController.GetTotalAmmoCount();
                weaponText.text = gunController.weaponName + ": " + ammoInMag + "/" + totalAmmo;
            }
        }
    }
}
