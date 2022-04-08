using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeployer : MonoBehaviour
{
    public GameObject objectPrefab;
    public int maxObjectCount;
    [SerializeField] float deployRange = 4f;

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

    // Deploy an object to a desired location on the scene.
    void DeployObject()
    {
        RaycastHit hit;

        // Cast a ray towards a direction
        if (Physics.Raycast(transform.position, transform.forward, out hit, deployRange))
        {
            // If the object is the Ground, then object can be deployed
            if (hit.transform.CompareTag("Ground"))
            {
                Instantiate(objectPrefab, hit.point, hit.transform.rotation);
                print("Deployment successful, # of remaining object: " + (objectCount - 1));
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

    // Restores the number of utility carried to the maximum possible.
    public void Restock()
    {
        objectCount = maxObjectCount;
    }

    // Return the current number of utility carried.
    public int GetObjectCount()
    {
        return objectCount;
    }

    // Return the name of the current utility carried.
    public string GetObjectName()
    {
        return objectPrefab.name;
    }
}
