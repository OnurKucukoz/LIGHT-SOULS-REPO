using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    public Transform cameraTransform; // Reference to the Cinemachine camera's transform

    private void Update()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal"); // Typically mapped to A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical"); // Typically mapped to W/S or Up/Down arrow keys

        // Calculate direction based on camera's forward direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Keep the direction parallel to the ground
        cameraForward.Normalize(); // Normalize the vector to ensure consistent movement speed

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Combine input direction with camera direction
        Vector3 movementDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Move the player
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        // Rotate the player to face the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}
