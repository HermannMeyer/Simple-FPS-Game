using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : GunController
{
    /*
    new public string weaponName = "Rocket Launcher";
    new public float fireRate = 300f; // Fire rate in terms of rounds per minute
    new public int ammoInMag; // Number of rounds in a magazine
    new int magazineSize = 1;
    new public int ammoCount; // Total number of rounds carried by the player
    new public int maxAmmoCount = 5;
    new public float reloadTime = 1f;
    new public Camera playerCamera;
    new public ParticleSystem muzzleFlash;
    new public Animator animator; */

    public GameObject projectilePrefab;
    public GameObject muzzle;

    float nextTimeToFire = 0f;
    bool isReloading = false;

    void Start()
    {
        Refill();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetButton("Fire1") && ammoInMag > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 60f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoInMag < magazineSize)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    new void Shoot()
    {
        // Muzzle Flash effect
        muzzleFlash.Play();

        // Instantiates a projectile
        Instantiate(projectilePrefab, muzzle.transform.position, muzzle.transform.rotation);

        ammoInMag--;
    }
}
