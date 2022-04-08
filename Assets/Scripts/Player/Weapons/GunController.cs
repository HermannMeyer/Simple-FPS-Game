using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] protected string weaponName = "Assault Rifle"; // Name of firearm
    [SerializeField] protected float damage = 5f; // Damage per round
    [SerializeField] protected float range = 100f; // Range of firearm
    [SerializeField] protected float fireRate = 300f; // Fire rate in terms of rounds per minute
    [SerializeField] protected float impactForce = 100f; // Force applied to an object for each round hit
    [SerializeField] protected int magazineSize = 30; // Size of magazine
    [SerializeField] protected int maxAmmoCount = 180; // Maximum amount of reserve ammo
    [SerializeField] protected float reloadTime = 1f; // Time to reload the magazine
    [SerializeField] protected ParticleSystem muzzleFlash; // Particle system to simulate the muzzle flash

    protected int ammoCount; // Total number of rounds carried by the player
    protected int ammoInMag; // Number of rounds in a magazine
    protected float nextTimeToFire = 0f; // Time between each round discharged
    protected bool isReloading = false; // 
    protected Camera playerCamera;
    protected Animator animator;

    // Awake is called as the script instance is loaded (before Start).
    protected void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = gameObject.GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update.
    protected void Start()
    {
        Refill();
    }

    // OnEnable is called when the object becomes enabled and active.
    protected void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

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

    // Discharge a round towards a desired direction.
    protected void Shoot()
    {
        // Muzzle Flash effect
        muzzleFlash.Play();

        RaycastHit hit;
        // Cast a ray from the gun's muzzle towards the point the gun is directed
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
        }
        // Subtract the amount of ammo in the magazine by 1
        ammoInMag--;
    }


    // Reload the gun and animate the process.
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

    // Restores the amounts of ammo both in the magazine and in reserve.
    public void Refill()
    {
        ammoCount = maxAmmoCount;
        ammoInMag = magazineSize;
    }

    // Return the current amount of ammo in the magazine.
    public int GetAmmoCountInMag()
    {
        return ammoInMag;
    }

    // Return the current amount of ammo in reserve.
    public int GetTotalAmmoCount()
    {
        return ammoCount;
    }

    // Return the weapon's name.
    public string GetWeaponName()
    {
        return weaponName;
    }
}
