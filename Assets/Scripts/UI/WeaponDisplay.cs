using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    TextMeshProUGUI weaponText;

    // Awake is called as the script instance is loaded (before Start).
    private void Awake()
    {
        weaponText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame.
    void Update()
    {
        GetCurrentWeapon();
    }

    // Obtain the name of the current weapon being held by the player and display its name and characteristics.
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
