using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : GunController
{
    [SerializeField] GameObject projectilePrefab; // Prefab of the launcher's projectile
    [SerializeField] GameObject muzzle; // Position of the muzzle
    [SerializeField] float initialForce = 0f; // Initial force to be applied to the projectile

    // Update is called once per frame.
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

    // Different version of Shoot, which discharges a physical projectile rather than a ray.
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
