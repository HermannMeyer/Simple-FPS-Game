using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] float throwForce = 50f;
    [SerializeField] int maxGrenadeCount = 4;
    [SerializeField] GameObject grenadePrefab;
    int grenadeCount;

    // Start is called before the first frame update
    void Start()
    {
        Restock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && (grenadeCount > 0))
        {
            ThrowGrenade();
            grenadeCount--;
        }
    }

    // Hurl the grenade towards a desired direction.
    void ThrowGrenade()
    {
        // Instantiate the grenade on scene
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        // Apply a throw force on the grenade
        rb.AddForce(throwForce * transform.forward);
    }

    // Restores the number of grenades carried to the maximum possible.
    public void Restock()
    {
        grenadeCount = maxGrenadeCount;
    }

    // Return the current number of grenades carried.
    public int GetGrenadeCount()
    {
        return grenadeCount;
    }
}
