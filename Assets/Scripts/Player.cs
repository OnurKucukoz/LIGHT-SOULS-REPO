using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    bool isDodging;
    float dodgeTimer;

    //Dodge upwards
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float verticalSpeed = 5.0f;

    private float horizontalInput;
    private float verticalInput;
    public float rotationSpeed = 10f;
    public bool isLocked = false;

    Animator animator;
    Vector3 direction;

    public Transform target;

    
    void Start()
    {
        isLocked = false;
        animator = GetComponent<Animator>();

       
    }

    
    void LateUpdate()
    {
        SwitchLockState();
    }

    bool isLightClicked = false;
    bool isHeavyClicked = false;
    bool isSpacePressed = false;

    //ABSTRACTION
    void FreelookPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && !isLightClicked && !isHeavyClicked)
        {
            StartCoroutine(StopPlayerMovementLight());
        }

        if (Input.GetMouseButtonDown(1) && !isHeavyClicked && !isLightClicked)
        {
            StartCoroutine(StopPlayerMovementHeavy());
        }

       
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

            direction = (forward * verticalInput + right * horizontalInput).normalized;

            if (direction != Vector3.zero)
            {
                //animator.SetBool("isWalking", true);
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            } 
    }

    

    IEnumerator StopPlayerMovementLight()
    {

        isLightClicked = true;
        speed = 0;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(1.44f);

        speed = 5;
        rotationSpeed = 10f;
        isLightClicked = false;
         
    }



    IEnumerator StopPlayerMovementHeavy()
    {
        isHeavyClicked = true;
        speed = 0;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(1.44f);

        speed = 5;
        rotationSpeed = 10f;
        isHeavyClicked = false;

    }


    IEnumerator StopPlayerMovementDodge()
    {

        isSpacePressed = true;
        speed = 1;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(1.20f);

        speed = 5;
        rotationSpeed = 10f;
        isSpacePressed = false;

    }

    void LockedOnPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && !isLightClicked && !isHeavyClicked)
        {
            StartCoroutine(StopPlayerMovementLight());
        }

        if (Input.GetMouseButtonDown(1) && !isHeavyClicked)
        {
            StartCoroutine(StopPlayerMovementHeavy());
        }

        if (Input.GetKeyDown("space") )
        {
            StartCoroutine(StopPlayerMovementDodge());
        }

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

       /* if (rightRelativeVerticalInput.magnitude != 0)
        {
            verticalSpeed = 0f;
        }
        else { verticalSpeed = 0f; }*/

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;
        transform.Translate(cameraRelativeMovement, Space.World);

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget.magnitude > 1f) // Avoid rotating if too close
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
