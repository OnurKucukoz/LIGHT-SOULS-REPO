using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    bool isDodging;
    float dodgeTimer;

    //Dodge upwards
    [SerializeField] private Rigidbody playerRb;
    public float speed = 2f;


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
    

    //ABSTRACTION
    void FreelookPlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StopPlayerMovementLight());
        }

        if (Input.GetMouseButtonDown(1) )
        {
            StartCoroutine(StopPlayerMovementHeavy());
        }

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(StopPlayerMovementDodge());
        }
        
        FreeLookRun();



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

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FreeLookRun()
    {
        if (Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked)
        {
            speed = 7f;
        }
        else if(!Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked)
        {
            speed = 2f;
        }
        
    }

    void LockedOnRun()
    {
        if (Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked)
        {
            speed = 5f;
        }
        else if (!Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked)
        {
            speed = 2f;
        }

    }

    IEnumerator StopPlayerMovementLight()
    {

        isLightClicked = true;
        speed = 0f;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(1.44f);

        speed = 2f;
        rotationSpeed = 10f;
        isLightClicked = false;

    }



    IEnumerator StopPlayerMovementHeavy()
    {
        isHeavyClicked = true;
        speed = 0f;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(1.66f);

        speed = 2f;
        rotationSpeed = 10f;
        isHeavyClicked = false;

    }


    IEnumerator StopPlayerMovementDodge()
    {

        
        speed = 1f;
        rotationSpeed = 3.20f;
        yield return new WaitForSeconds(1.20f);

        speed = 2f;
        rotationSpeed = 10f;
        

    }

    void LockedOnPlayerMovement()
    {
        Vector3 directionToEnemy = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (Input.GetMouseButtonDown(0) && !isLightClicked && !isHeavyClicked)
        {
            StartCoroutine(StopPlayerMovementLight());
        }

        if (Input.GetMouseButtonDown(1) && !isHeavyClicked)
        {
            StartCoroutine(StopPlayerMovementHeavy());
        }

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(StopPlayerMovementDodge());
        }

        LockedOnRun();

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
