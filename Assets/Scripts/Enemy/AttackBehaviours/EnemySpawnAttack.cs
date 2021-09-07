using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnAttack : MonoBehaviour
{
    public float cooldown = 1f; // Rate of attack (per second)
    public float damage = 10f;
    public GameObject spawnlingPrefab;
    public Target target;
    public int numberOfSpawnlings = 4;

    GameObject player;
    PlayerHealth playerHealth;
    bool isInRange = false;
    bool isDead = false;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        countdown = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;

        if (countdown >= cooldown && isInRange)
        {
            Attack();
        }

        if (target.health <= 0 && !isDead)
        {
            Spawn();
            isDead = true;
        }
    }

    void Attack()
    {
        countdown = 0;

        playerHealth.TakeDamage(damage);
    }

    void Spawn()
    {
        Vector3[] spawnPositions = { transform.position + Vector3.forward, transform.position + Vector3.back,
                                    transform.position + Vector3.left, transform.position + Vector3.right };

        for (int i = 0; i < numberOfSpawnlings; i++)
        {
            int positionIndex = i % numberOfSpawnlings;
            Instantiate(spawnlingPrefab, spawnPositions[positionIndex], transform.rotation);

        }
    }
}
