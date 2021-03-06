using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    GameObject player;
    Vector3 curPosition;

    // Start is called before the first frame update.
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
