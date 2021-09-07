using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    public NavMeshAgent agent;

    GameObject player;

    Vector3 curPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the player
        curPosition = player.transform.position;

        // Set the destination to the current position of the player
        agent.SetDestination(curPosition);
    }
}
