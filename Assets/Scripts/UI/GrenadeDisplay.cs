using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrenadeDisplay : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    TextMeshProUGUI text;

    // Awake is called as the script instance is loaded (before Start).
    void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GrenadeThrower grenadeThrower = mainCamera.GetComponent<GrenadeThrower>();
        int grenadeCount = grenadeThrower.GetGrenadeCount();
        // Display the current number of grenades.
        text.text = "Grenade: " + grenadeCount;
    }
}
