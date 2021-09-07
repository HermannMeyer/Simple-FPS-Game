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
        text.text = "Grenade: " + grenadeThrower.grenadeCount;
    }
}
