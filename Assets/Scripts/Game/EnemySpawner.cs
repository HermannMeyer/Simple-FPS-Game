using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefabs for each enemy type.
    public GameObject gruntPrefab;
    public GameObject brutePrefab;
    public GameObject exploderPrefab;
    public GameObject carrierPrefab;

    Transform[] spawnPoints; // Array containing all spawn locations.
    float timeBetweenSpawns = 5f; // Time between each enemy spawning.

    // Maximum number of each type allowed to be spawned during each wave.
    int maxGruntCount = 2;
    int maxBruteCount = 0;
    int maxExploderCount = -1;
    int maxCarrierCount = -1;
    // Current number of each type to be spawned in each wave.
    int gruntCount = 2;
    int bruteCount = 0;
    int exploderCount = -1;
    int carrierCount = -1;
   
    // Start is called before the first frame update.
    void Start()
    {
        FindAllSpawnPoints();   
    }

    // Find all spawn points on the level.
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

    // Increase the number of enemy NPCs to be spawned in each wave.
    public void AddMoreEnemies()
    {
        maxGruntCount += 2;
        maxBruteCount += 1;
        maxExploderCount += 1;
        maxCarrierCount += 1;
    }
    
    // Spawn enemy NPCs.
    public void SpawnEnemies()
    {
        // Spawn each type of NPC.
        InvokeRepeating("SpawnGrunts", 0f, timeBetweenSpawns);
        InvokeRepeating("SpawnBrutes", 4f, timeBetweenSpawns);
        InvokeRepeating("SpawnExploders", 2f, timeBetweenSpawns);
        InvokeRepeating("SpawnCarriers", 6f, timeBetweenSpawns);
    }

    // Update the current number of enemy NPCs to be spawned to the maximum number.
    public void UpdateCounts()
    {
        gruntCount = maxGruntCount;
        bruteCount = maxBruteCount;
        exploderCount = maxExploderCount;
        carrierCount = maxCarrierCount;
    }

    // Reset the number of enemy NPCs to be spawned to zero.
    public void ResetCounts()
    {
        gruntCount = 0;
        bruteCount = 0;
        exploderCount = 0;
        carrierCount = 0;
    }

    // Determine whether all enemy NPCs have been completely spawned or not.
    public bool hasSpawned()
    {
        if (gruntCount == 0 && bruteCount == 0 && exploderCount == 0 && carrierCount == 0)
        {
            return true;
        }

        return false;
    }

    // Spawn a Grunt at a random location chosen amongst the pool of potential spawn points.
    void SpawnGrunts()
    {
        if (gruntCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(gruntPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        gruntCount--;
    }

    // Spawn a Brute at a random location chosen amongst the pool of potential spawn points.
    void SpawnBrutes()
    {
        if (bruteCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(brutePrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        bruteCount--;
    }

    // Spawn an Exploder at a random location chosen amongst the pool of potential spawn points.
    void SpawnExploders()
    {
        if (exploderCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(exploderPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        exploderCount--;
    }

    // Spawn a Carrier at a random location chosen amongst the pool of potential spawn points.
    void SpawnCarriers()
    {
        if (carrierCount <= 0) return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(carrierPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        carrierCount--;
    }
}
