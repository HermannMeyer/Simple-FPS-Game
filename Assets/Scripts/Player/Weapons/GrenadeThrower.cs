using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 50f;
    public int maxGrenadeCount = 4;
    public GameObject grenadePrefab;
    int grenadeCount;

    void Start()
    {
        Restock();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && (grenadeCount > 0))
        {
            ThrowGrenade();
            grenadeCount--;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(throwForce * transform.forward);
    }

    public void Restock()
    {
        grenadeCount = maxGrenadeCount;
    }

    public int GetGrenadeCount()
    {
        return grenadeCount;
    }
}
