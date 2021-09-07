using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 50f;
    public int maxGrenadeCount = 4;
    public GameObject grenadePrefab;
    public int grenadeCount;

    void Start()
    {
        Restock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && maxGrenadeCount > 0)
        {
            ThrowGrenade();
            grenadeCount -= 1;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(throwForce * transform.forward);
    }

    void Restock()
    {
        grenadeCount = maxGrenadeCount;
    }
}
