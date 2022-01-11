using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : GunController
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject muzzle;
    [SerializeField] float initialForce = 0f;

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetButton("Fire1") && (ammoInMag > 0) && (Time.time >= nextTimeToFire))
        {
            nextTimeToFire = Time.time + 60f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && (ammoInMag < magazineSize))
        {
            StartCoroutine(Reload());
            return;
        }
    }

    new void Shoot()
    {
        // Muzzle Flash effect
        muzzleFlash.Play();

        // Instantiates a projectile and apply initial force if applicable
        GameObject projectile = Instantiate(projectilePrefab, muzzle.transform.position, muzzle.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(initialForce * muzzle.transform.forward);

        ammoInMag--;
    }
}
