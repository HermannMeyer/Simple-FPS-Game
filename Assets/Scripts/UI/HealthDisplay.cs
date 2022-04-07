using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Transform player;
    TextMeshProUGUI healthText;

    // Awake is called as the script instance is loaded (before Start).
    private void Awake()
    {
        healthText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame.
    void Update()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        // Display the current health of the player
        healthText.text = "Health: " + playerHealth.GetHealth().ToString();
    }
}
