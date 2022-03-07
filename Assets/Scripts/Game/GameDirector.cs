using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameDirector : MonoBehaviour
{
    public int currentWave;
    public int maxWave = 10;
    public float timeBetweenWaves = 10f;
    public float waveCooldown;
    public int enemiesRemaining;
    public EnemySpawner enemySpawner;
    [SerializeField] GameObject waveDisplayObj;
    [SerializeField] GameObject waveTimerObj;

    GameObject player;
    PlayerHealth health;
    TextMeshProUGUI waveTimer;
    TextMeshProUGUI waveDisplay;

    bool waveInProgress;
    bool hasSpawnedEnemies;
    bool gameOver;
    GameObject[] enemies;

    void Awake()
    {
        if (enemySpawner == null)
        {
            enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        }
        waveTimer = waveTimerObj.GetComponent<TextMeshProUGUI>();
        waveDisplay = waveDisplayObj.GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        waveCooldown = timeBetweenWaves;
        waveInProgress = false;
        hasSpawnedEnemies = false;
        gameOver = false;

        // Reset the current enemy count to zero
        enemySpawner.ResetCounts();

        // Update the wave UI
        UpdateWaveUI();
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is over, end the game (for now)
        if (gameOver)
        {
            EndGame();
        }

        // Else we continue on with the game
        else
        {
            if (waveInProgress)
            {
                // If the enemies are not spawning
                if (!hasSpawnedEnemies)
                {
                    // Set this to true so that the following lines cannot be called more than once for this wave
                    hasSpawnedEnemies = true;
                    // Increase the max number of enemies to spawn for the new wave
                    enemySpawner.AddMoreEnemies();
                    // Update the current enemy count so that the spawner can spawn more
                    enemySpawner.UpdateCounts();
                    enemySpawner.SpawnEnemies();
                }

                // Check if there are no more enemies on the scene
                if (enemySpawner.hasSpawned())
                {
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    enemiesRemaining = enemies.Length;

                    if (enemiesRemaining <= 0)
                    {
                        waveInProgress = false;
                        hasSpawnedEnemies = false;
                        enemySpawner.ResetCounts();
                    }
                }
            }

            else
            {
                waveCooldown -= Time.deltaTime;
                // Run the timer UI
                if (!waveTimer.gameObject.activeSelf)
                {
                    waveTimer.gameObject.SetActive(true);
                }
                waveTimer.text = waveCooldown.ToString("F1") + " seconds before next wave!";


                // If the countdown for next wave is up and wave is not in progress
                // Set wave to be in progress
                if (waveCooldown <= 0)
                {
                    waveInProgress = true;
                    currentWave++;
                    waveCooldown = timeBetweenWaves;

                    // Disable the timer UI
                    if (waveTimer.gameObject.activeSelf)
                    {
                        waveTimer.gameObject.SetActive(false);
                    }

                    // Update the wave UI
                    UpdateWaveUI();
                }
            }
        }

        // Check if either the current wave is more than the max wave or the player has died
        if (currentWave > maxWave || health.hasDied)
        {
            gameOver = true;
            print("Game Over!");
            print("Wave " + currentWave + "/" + maxWave);
        }
    }

    void UpdateWaveUI()
    {
        waveDisplay.text = "Wave: " + currentWave + "/" + maxWave;
    }

    void EndGame()
    {
        // TODO - Display game over scene
        // For now, this will simply quit the game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    
    
}
