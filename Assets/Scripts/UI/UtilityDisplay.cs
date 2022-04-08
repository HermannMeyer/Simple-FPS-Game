using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UtilityDisplay : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    TextMeshProUGUI text;

    // Awake is called as the script instance is loaded (before Start).
    void Awake() 
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame.
    void Update()
    {
        ObjectDeployer objectDeployer = mainCamera.GetComponent<ObjectDeployer>();
        // Display the current utility the player has and the number available.
        text.text = objectDeployer.GetObjectName() + ": " + objectDeployer.GetObjectCount();
    }
}