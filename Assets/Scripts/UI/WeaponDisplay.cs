using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    // public GameObject weaponHolder;
    [SerializeField] Text weaponText;
    [SerializeField] Transform weaponHolder;

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
                string ammoInMag = gunController.GetAmmoCountInMag().ToString();
                string totalAmmo = gunController.GetTotalAmmoCount().ToString();
                string weaponName = gunController.GetWeaponName();
                weaponText.text = weaponName + ": " + ammoInMag + "/" + totalAmmo;
            }
        }
    }
}
