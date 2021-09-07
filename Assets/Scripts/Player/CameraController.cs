using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 100f;
    public Transform playerTransform;

    private float horizontalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        // Get the mouse input from the X and Y axes
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;

        // Calculate the current X rotation and clamp it
        horizontalRotation -= mouseY;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -89f, 89f);

        // Rotate the player character around the Y axis
        playerTransform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the X axis
        transform.localRotation = Quaternion.Euler(Vector3.right * horizontalRotation);
    }
}
