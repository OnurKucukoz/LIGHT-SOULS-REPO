using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 5.0f;


    private float horizontalInput;
    private float verticalInput;

    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        PlayerMovement();
        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    //ABSTRACTION
    void PlayerMovement()
    {
        transform.rotation = Camera.main.transform.rotation;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward=forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = verticalInput * forward * Time.deltaTime * speed;
        Vector3 rightRelativeVerticalInput = horizontalInput * right * Time.deltaTime * speed;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeVerticalInput;
        transform.Translate(cameraRelativeMovement, Space.World);


        //transform.Translate(horizontalInput * Time.deltaTime * Vector3.right * speed);
        //transform.Translate(verticalInput * Time.deltaTime * Vector3.forward * speed);


    }

    
}
