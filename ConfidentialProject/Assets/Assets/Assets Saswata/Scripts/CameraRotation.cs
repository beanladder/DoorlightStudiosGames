using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Sensitivity for mouse rotation
    public float rotationSpeed = 5f;

    // Flag to check if the cursor is locked
    private bool cursorLocked = false;

    // Flag to check if the right mouse button is held down
    private bool isRightMouseDown = false;

    void Start()
    {
        transform.rotation = Quaternion.Euler(45f, 45f, 0f);
    }

    void Update()
    {
        // Check for right mouse button press
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseDown = true;
        }

        // Check for right mouse button release
        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseDown = false;

            // Optional: Lock the cursor when the button is released
            Cursor.lockState = CursorLockMode.None;
        }

        // Rotate the camera if right mouse button is held down
        if (isRightMouseDown)
        {
            // Optional: Unlock the cursor when the button is held down
            cursorLocked = true;
            Cursor.lockState = CursorLockMode.Locked;

            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X");

            // Calculate the rotation amount
            float rotationAmount = mouseX * rotationSpeed * Time.deltaTime;

            // Create a new rotation based on the y-axis input
            Quaternion yRotation = Quaternion.Euler(0f, rotationAmount, 0f);

            // Apply the rotation to the camera's y-axis
            transform.rotation *= yRotation;

            // Ensure x-axis rotation remains fixed at 45 degrees
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.x = 45f;

            // Ensure z-axis rotation remains fixed at 0 degrees
            currentRotation.z = 0f;

            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
