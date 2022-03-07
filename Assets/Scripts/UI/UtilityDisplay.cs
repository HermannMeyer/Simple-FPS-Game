using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UtilityDisplay : MonoBehaviour
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
        ObjectDeployer objectDeployer = mainCamera.GetComponent<ObjectDeployer>();
        text.text = objectDeployer.GetObjectName() + ": " + objectDeployer.GetObjectCount();
    }
}