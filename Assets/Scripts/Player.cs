using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody playerRb;
    public float speed = 2f;


    private float horizontalInput;
    private float verticalInput;
    public float rotationSpeed = 10f;
    public bool isLocked = false;



    Vector3 direction;

    public Transform target;
    public AnimationStateController animationStateController;
 

    void Start()
    {
        isLocked = false;        
        animationStateController =GameObject.Find("Player").GetComponent<AnimationStateController>();
    }


    void LateUpdate()
    {
        if (!PauseMenu.isPaused)
        {
            SwitchLockState();
        }
    }

    bool isLightClicked = false;
    bool isHeavyClicked = false;
    bool isHealClicked = false;
    

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

        if(Input.GetKeyDown("r"))
        {
            StartCoroutine(StopPlayerMovementHeal());
        }
        //ABSTRACTION
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

    void FreeLookRun()//ABSTRACTION
    {
        if (Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked && !isHealClicked && (animationStateController.stamina > 12))
        {
            animationStateController.stamina -= 20 * Time.deltaTime;
            speed = 5f;
        }
        else if(!Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked && !isHealClicked && (animationStateController.stamina > 11))
        {
            
            speed = 2f;
        }
        else if (animationStateController.stamina <= 11)
        {
            speed = 1f; 
        }

    }

    void LockedOnRun()
    {
        if (Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked && !isHealClicked && (animationStateController.stamina > 3))
        {
            animationStateController.stamina -= 12 * Time.deltaTime;
            speed = 4f;
        }
        else if (!Input.GetKey("left shift") && !isLightClicked && !isHeavyClicked && !isHealClicked)
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

    IEnumerator StopPlayerMovementHeal()
    {
    if (animationStateController.medicineCount > 0)
    {
        isHealClicked = true;
        speed = 0f;
        rotationSpeed = 1.20f;
        yield return new WaitForSeconds(2.65f);

        speed = 2f;
        rotationSpeed = 10f;
        isHealClicked = false;
    }
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

        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(StopPlayerMovementHeal());
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
        if (!PauseMenu.isPaused)
        {
            // Check for "F" key press to toggle lock-on state
            if (Input.GetKeyDown(KeyCode.F))
            {
                isLocked = !isLocked;
            }

            // Call the appropriate movement method based on isLocked state
            if (isLocked)
            {
                // enemyFocusPoint.enabled = true;

                //turn on knob
                LockedOnPlayerMovement();
            }
            else
            {
                // enemyFocusPoint.enabled = false;

                //turn off knob
                FreelookPlayerMovement();
            }
        }
    }

}
