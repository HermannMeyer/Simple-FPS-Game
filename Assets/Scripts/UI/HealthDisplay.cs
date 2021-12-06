using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Text healthText;
    [SerializeField] Transform player;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        healthText.text = "Health: " + playerHealth.GetHealth().ToString();
    }
}
