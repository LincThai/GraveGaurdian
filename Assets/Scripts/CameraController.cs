using UnityEngine;

public class CameraController : MonoBehaviour
{
    // set variables
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Locks cursor so when you move the cursor you don't see it.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse input from the x and y axis assigned to variables
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        // Assign a new value every update to xRotation equal to mouseY
        xRotation -= mouseY;
        // Clamp the value of xRotation between -90 and 90 degrees stops player from turning the camera too far up or down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applies the up and down rotation on the x axis to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // Applies the left and right rotation to the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
