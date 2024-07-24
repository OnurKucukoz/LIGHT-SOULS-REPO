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
    void FixedUpdate()
    {
        PlayerMovement();
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);

    }

    //ABSTRACTION
    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(horizontalInput * Time.deltaTime * Vector3.right * speed);
        transform.Translate(verticalInput * Time.deltaTime * Vector3.forward * speed);

       
    }

    
}
