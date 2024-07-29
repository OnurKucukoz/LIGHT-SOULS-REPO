using UnityEngine;
using Cinemachine;
using System;

public class FocusOnEnemy : MonoBehaviour
{
    public GameObject player; // The player's transform
    public float distanceToPlayer = 5;

    public Transform target; // The target (enemy) transform
    //public CinemachineFreeLook freeLookCamera; // The Cinemachine FreeLook camera
    public float lockOnOffsetDistance = 5f; // Distance of the camera behind the player
    public bool isLockedOn = false; // Whether the player is locked onto a target

    // private Vector3 originalOffset; // Original camera offset
    //private float originalYAxisValue; // Original Y Axis value for Cinemachine



    private void Start()
    {
        /*        if (freeLookCamera == null)
                {
                    freeLookCamera = GetComponent<CinemachineFreeLook>();
                }
                // Store the original settings
                originalOffset = freeLookCamera.m_Lens.OrthographicSize * Vector3.back;
                originalYAxisValue = freeLookCamera.m_YAxis.Value;*/
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLockedOn = true;
            LockCamera();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            isLockedOn = false;
            
        }

        if (isLockedOn && target != null)
        {
            
            // Optionally, control Cinemachine FreeLook rig values if using one
            /* if (freeLookCamera != null)
             {
                 freeLookCamera.m_YAxis.Value = Mathf.Lerp(freeLookCamera.m_YAxis.Value, 0.5f, Time.deltaTime);
                 // Additional settings can be adjusted here
             }
         }
         else
         {
             // Free camera movement logic (e.g., user control)
             float horizontalInput = Input.GetAxis("Mouse X");
             freeLookCamera.m_XAxis.Value += horizontalInput * Time.deltaTime * 100f;

             // Restore original Cinemachine settings if needed
             freeLookCamera.m_YAxis.Value = originalYAxisValue;
         }
     }*/
        }
    }

    private void LockCamera()
    {
        if (isLockedOn && target != null)
        {
            /*// Adjust the player's rotation to face the target
            Vector3 directionToTarget = (target.position - player.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            player.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

            // Position the camera behind the player
            Vector3 cameraPosition = player.position - player.forward * lockOnOffsetDistance;
            transform.position = cameraPosition;
            transform.LookAt(target.position);*/
/*
            transform.position = player.transform.position - player.transform.forward * distanceToPlayer;
            transform.LookAt(player.transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);*/
            
        }
    }
}