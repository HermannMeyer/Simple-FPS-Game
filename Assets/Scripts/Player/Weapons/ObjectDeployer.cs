using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeployer : MonoBehaviour
{
    public GameObject objectPrefab;
    public int maxObjectCount;
    public float deployRange = 4f;

    int objectCount;

    // Start is called before the first frame update
    void Start()
    {
        Restock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && (objectCount > 0))
        {
            DeployObject();
            objectCount--;
        } 
    }

    void DeployObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, deployRange))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                Instantiate(objectPrefab, hit.point, hit.transform.rotation);
                print("Deployment successful, # of remaining object: " + objectCount);
            }
            else
            {
                print("Deployment target is not ground, unable to deploy!");
            }
        }
        else
        {
            print("Ray failed to hit anything, unable to deploy!");
        }
    }

    void Restock()
    {
        objectCount = maxObjectCount;
    }

    public int GetObjectCount()
    {
        return objectCount;
    }
}
