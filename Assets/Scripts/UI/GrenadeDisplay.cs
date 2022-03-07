using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrenadeDisplay : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    TextMeshProUGUI text;

    void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GrenadeThrower grenadeThrower = mainCamera.GetComponent<GrenadeThrower>();
        int grenadeCount = grenadeThrower.GetGrenadeCount();
        text.text = "Grenade: " + grenadeCount;
    }
}
