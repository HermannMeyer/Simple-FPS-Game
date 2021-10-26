using UnityEngine;
using UnityEngine.UI;

public class GrenadeDisplay : MonoBehaviour
{
    public Camera mainCamera;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        GrenadeThrower grenadeThrower = mainCamera.GetComponent<GrenadeThrower>();
        int grenadeCount = grenadeThrower.GetGrenadeCount();
        text.text = "Grenade: " + grenadeCount;
    }
}
