using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    // public GameObject weaponHolder;
    [SerializeField] Transform weaponHolder;
    TextMeshProUGUI weaponText;

    private void Awake()
    {
        weaponText = GetComponent<TextMeshProUGUI>();
    }

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
