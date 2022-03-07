using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Transform player;
    TextMeshProUGUI healthText;

    private void Awake()
    {
        healthText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        healthText.text = "Health: " + playerHealth.GetHealth().ToString();
    }
}
