using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    CameraSwitcher cameraSwitcher;
    Animator animator;
    public bool isFreeLookActive;
    
    int isWalkingHash;
    int isRunningHash;
   

    int isLockedOnWalkingForwardHash;
    int isLockedOnWalkingBackwardHash;
    int isLockedOnWalkingRightHash;
    int isLockedOnWalkingLeftHash;
    int isLockedOnRunningForwardHash;
    int isLockedOnRunningBackwardHash;
    int isLockedOnLeftStrafingHash;
    int isLockedOnRightStrafingHash;

    int isDodgingHash;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraSwitcher = GameObject.Find("CameraSwitcher").GetComponent<CameraSwitcher>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        

        isLockedOnWalkingForwardHash = Animator.StringToHash("isLockedOnWalkingForward");
        isLockedOnWalkingBackwardHash = Animator.StringToHash("isLockedOnWalkingBackward");
        isLockedOnWalkingRightHash = Animator.StringToHash("isLockedOnWalkingRight");
        isLockedOnWalkingLeftHash = Animator.StringToHash("isLockedOnWalkingLeft");

        isLockedOnRunningForwardHash = Animator.StringToHash("isLockedOnRunningForward");
        isLockedOnRunningBackwardHash = Animator.StringToHash("isLockedOnRunningBackward");
        isLockedOnLeftStrafingHash = Animator.StringToHash("isLockedOnLeftStrafing");
        isLockedOnRightStrafingHash = Animator.StringToHash("isLockedOnRightStrafing");

        isDodgingHash = Animator.StringToHash("isDodging");
    }

    // Update is called once per frame
    void Update()
    {
        isFreeLookActive = cameraSwitcher.isFreeLookActive;
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        bool isLockedOnWalkingForward = animator.GetBool(isLockedOnWalkingForwardHash);
        bool isLockedOnWalkingBackward = animator.GetBool(isLockedOnWalkingBackwardHash);
        bool isLockedOnWalkingRight = animator.GetBool(isLockedOnWalkingRightHash);
        bool isLockedOnWalkingLeft = animator.GetBool(isLockedOnWalkingLeftHash);

        bool isLockedOnRunningForward = animator.GetBool(isLockedOnRunningForwardHash);
        bool isLockedOnRunningBackward = animator.GetBool(isLockedOnRunningBackwardHash);
        bool isLockedOnLeftStrafing = animator.GetBool(isLockedOnLeftStrafingHash);
        bool isLockedOnRightStrafing = animator.GetBool(isLockedOnRightStrafingHash);

        bool isDodging = animator.GetBool(isDodgingHash);



        bool shiftPressed = Input.GetKey("left shift");
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool spacePressed = Input.GetKeyDown("space");
        

        //start freelook walking
        if(isFreeLookActive && !isWalking && (forwardPressed || rightPressed || leftPressed || backPressed))
        {
            animator.SetBool(isWalkingHash, true);
        }
        //stop freelook walking
        if (!isFreeLookActive || (isWalking && !(forwardPressed || rightPressed || leftPressed || backPressed)))
        {
            animator.SetBool(isWalkingHash, false);
        }

        //start freelook running
        if (isFreeLookActive && !isRunning && ((forwardPressed || rightPressed || leftPressed || backPressed) && shiftPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        //stop freelook running
        if (!isFreeLookActive || (isRunning && (!(forwardPressed || rightPressed || leftPressed || backPressed) || !shiftPressed)))
        {
            animator.SetBool(isRunningHash, false);
        }

        //start freelook rolling
        if(spacePressed)
        {
            animator.SetBool(isDodgingHash, true);
        }


        //start locked on walk forward
        if (!isFreeLookActive && !isLockedOnWalkingForward && forwardPressed ) 
        {
            animator.SetBool(isLockedOnWalkingForwardHash, true);
        }

        //stop locked on walk forward
        if(isFreeLookActive || (isLockedOnWalkingForward && !forwardPressed))
        {
            animator.SetBool(isLockedOnWalkingForwardHash, false);
        
        }


        //start locked on walk backward
        if(!isFreeLookActive && !isLockedOnWalkingBackward && backPressed)
        {
            animator.SetBool(isLockedOnWalkingBackwardHash, true);
        
        }
        //stop locked on walk backward
        if(isFreeLookActive || (isLockedOnWalkingBackward && !backPressed))
        {
            animator.SetBool(isLockedOnWalkingBackwardHash, false);
        
        }


        //start locked on right strafe walking
        if(!isFreeLookActive && !isLockedOnWalkingRight && rightPressed)
        {
            animator.SetBool(isLockedOnWalkingRightHash, true);
        
        }
        //stop locked on right strafe walking
        if(isFreeLookActive || (isLockedOnWalkingRight && !rightPressed))
        {
            animator.SetBool(isLockedOnWalkingRightHash, false);
        
        }


        //start locked on left strafe walking
        if(!isFreeLookActive && !isLockedOnWalkingLeft && leftPressed)
        {
            animator.SetBool(isLockedOnWalkingLeftHash, true);

        }
        //stop locked on left strafe walking
        if(isFreeLookActive || (isLockedOnWalkingLeft && !leftPressed))
        {
            animator.SetBool(isLockedOnWalkingLeftHash, false);

        }


        //start locked on forward running -onur sana bir not gecmisteki onurdan. startlarda hepsi && li olacak. Stoplarda isfreelook||(....&&!...)
        if(!isFreeLookActive && !isLockedOnRunningForward && forwardPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRunningForwardHash, true);
        
        }
        //stop locked on forward running 
        if(isFreeLookActive || (isLockedOnRunningForward && (!forwardPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnRunningForwardHash, false);

        }


        //start locked on backward running 
        if(!isFreeLookActive && !isLockedOnRunningBackward && backPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRunningBackwardHash, true);

        }
        //stop locked on backward running 
        if(isFreeLookActive || (isLockedOnRunningBackward && (!backPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnRunningBackwardHash, false);

        }


        //start locked on right strafing
        if(!isFreeLookActive && !isLockedOnRightStrafing && rightPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRightStrafingHash, true);


        }
        //stop locked on right strafing
        if(isFreeLookActive || (isLockedOnRightStrafing && (!rightPressed || !shiftPressed)))
        {

            animator.SetBool(isLockedOnRightStrafingHash, false);
        }


        //start locked on left strafing
        if(!isFreeLookActive && !isLockedOnLeftStrafing && leftPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnLeftStrafingHash, true);

        }
        //stop locked on left strafing
        if(isFreeLookActive || (isLockedOnLeftStrafing && (!leftPressed || !shiftPressed)))
        {

            animator.SetBool(isLockedOnLeftStrafingHash, false);

        }


        //start locked on rolling
        if(!isFreeLookActive)
        {

        
        }






    }
}
       /* if (isFreeLookActive && shiftPressed && !isWalking && (forwardPressed || rightPressed || leftPressed || backPressed))
        {
            animator.SetBool(isRunningHash, true);
        }*/

       /* if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }*/

        /*if(isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }*/

       /* if (!isRunning && (forwardPressed && shiftPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !shiftPressed))
        {
            animator.SetBool(isRunningHash, false);
        }*/