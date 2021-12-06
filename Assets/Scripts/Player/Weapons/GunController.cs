using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] protected string weaponName = "Assault Rifle";
    [SerializeField] protected float damage = 5f;
    [SerializeField] protected float range = 100f;
    [SerializeField] protected float fireRate = 300f; // Fire rate in terms of rounds per minute
    [SerializeField] protected float impactForce = 100f;
    [SerializeField] protected int magazineSize = 30;
    [SerializeField] protected int maxAmmoCount = 180;
    [SerializeField] protected float reloadTime = 1f;
    [SerializeField] protected Camera playerCamera;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected Animator animator;

    protected int ammoCount; // Total number of rounds carried by the player
    protected int ammoInMag; // Number of rounds in a magazine
    protected float nextTimeToFire = 0f;
    protected bool isReloading = false;

    void Start()
    {
        Refill();
    }

    protected void OnEnable()
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

    protected void Shoot()
    {
        // Muzzle Flash effect
        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Hit effect - TODO
        }

        ammoInMag--;
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);

        ammoCount += ammoInMag;
        
        if (ammoCount >= magazineSize)
        {
            ammoCount -= magazineSize;
            ammoInMag = magazineSize;
        }
        else
        {
            ammoInMag = ammoCount;
            ammoCount = 0;
        }

        isReloading = false;
    }

    public void Refill()
    {
        ammoCount = maxAmmoCount;
        ammoInMag = magazineSize;
    }

    public int GetAmmoCountInMag()
    {
        return ammoInMag;
    }

    public int GetTotalAmmoCount()
    {
        return ammoCount;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }
}
