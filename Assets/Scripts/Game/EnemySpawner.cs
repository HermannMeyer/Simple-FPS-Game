using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject gruntPrefab;
    public GameObject brutePrefab;
    public GameObject exploderPrefab;
    public GameObject carrierPrefab;

    Transform[] spawnPoints;
    float timeBetweenSpawns = 5f;

    int maxGruntCount = 2;
    int maxBruteCount = 0;
    int maxExploderCount = -1;
    int maxCarrierCount = -1;
    int gruntCount = 2;
    int bruteCount = 0;
    int exploderCount = -1;
    int carrierCount = -1;
    // A struct to represent enemies?
   
    // Start is called before the first frame update
    void Start()
    {
        FindAllSpawnPoints();   
    }

    void FindAllSpawnPoints()
    {
        int arraySize = transform.childCount;
        spawnPoints = new Transform[arraySize];
        int index = 0;

        foreach (Transform spawnPoint in transform)
        {
            spawnPoints[index] = spawnPoint;
            index++;
        }
    }

    public void AddMoreEnemies()
    {
        // Placeholder code with simple spawning behaviour
        maxGruntCount += 2;
        maxBruteCount += 1;
        maxExploderCount += 1;
        maxCarrierCount += 1;
    }
     
    public void SpawnEnemies()
    {
        InvokeRepeating("SpawnGrunts", 0f, timeBetweenSpawns);
        InvokeRepeating("SpawnBrutes", 4f, timeBetweenSpawns);
        InvokeRepeating("SpawnExploders", 2f, timeBetweenSpawns);
        InvokeRepeating("SpawnCarriers", 6f, timeBetweenSpawns);
    }

    public void UpdateCounts()
    {
        gruntCount = maxGruntCount;
        bruteCount = maxBruteCount;
        exploderCount = maxExploderCount;
        carrierCount = maxCarrierCount;
    }
    public void ResetCounts()
    {
        gruntCount = 0;
        bruteCount = 0;
        exploderCount = 0;
        carrierCount = 0;
    }

    public bool hasSpawned()
    {
        if (gruntCount == 0 && bruteCount == 0 && exploderCount == 0 && carrierCount == 0)
        {
            return true;
        }

        return false;
    }

    // The following lines are placeholders

    void SpawnGrunts()
    {
        if (gruntCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(gruntPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        gruntCount--;
    }

    void SpawnBrutes()
    {
        if (bruteCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(brutePrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        bruteCount--;
    }

    void SpawnExploders()
    {
        if (exploderCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(exploderPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        exploderCount--;
    }

    void SpawnCarriers()
    {
        if (carrierCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(carrierPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        carrierCount--;
    }
}
