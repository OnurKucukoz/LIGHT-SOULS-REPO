using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float verticalSpeed = 5.0f;


    private float horizontalInput;
    private float verticalInput;


    public float rotationSpeed = 10f;
    public bool isLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SwitchLockState();
    }

    //ABSTRACTION
    void FreelookPlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = verticalInput * forward * Time.deltaTime * speed;
        Vector3 rightRelativeVerticalInput = horizontalInput * right * Time.deltaTime * speed;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;
        transform.Translate(cameraRelativeMovement, Space.World);

        Vector3 direction = (forward * verticalInput + right * horizontalInput).normalized;

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        


    }
    public Transform target;
    void LockedOnPlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = verticalInput * forward * Time.deltaTime * verticalSpeed;
        Vector3 rightRelativeVerticalInput = horizontalInput * right * Time.deltaTime * speed;

        if(rightRelativeVerticalInput.magnitude != 0)
        {
            verticalSpeed = 3f;
        }
        else { verticalSpeed = 5f; }



        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;
        transform.Translate(cameraRelativeMovement, Space.World);



        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget.magnitude > 0.1f) // Avoid rotating if too close
        {
            Quaternion toRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void SwitchLockState()
    {
        // Check for "F" key press to toggle lock-on state
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLocked = !isLocked;
        }

        // Call the appropriate movement method based on isLocked state
        if (isLocked)
        {
            LockedOnPlayerMovement();
        }
        else
        {
            FreelookPlayerMovement();
        }

    }


}
