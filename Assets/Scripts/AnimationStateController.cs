using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Player player;
    CameraSwitcher cameraSwitcher;
    Animator animator;
    public bool isFreeLookActive;
    //private Player player;

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

    int isAttackingLight;
    int isAttackingHeavy;

    public bool isDodging;
    public float cooldownTimer = 0;

    void Start()
    {
        cameraSwitcher = GameObject.Find("CameraSwitcher").GetComponent<CameraSwitcher>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

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

        isAttackingLight = Animator.StringToHash("isAttackingLight");
        isAttackingHeavy = Animator.StringToHash("isAttackingHeavy");
    }


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

        bool shiftPressed = Input.GetKey("left shift");
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool spacePressed = Input.GetKeyDown("space");

        bool leftMouseClicked = Input.GetMouseButtonDown(0);
        bool rightMouseClicked = Input.GetMouseButton(1);

        bool isLightAttacking = false;
        bool isHeavyAttacking = false;


        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (isFreeLookActive && leftMouseClicked && !isLightAttacking)
        {

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.30f;
               FreeLookLightAttack();
            }
        }

        if (!isFreeLookActive && leftMouseClicked && !isLightAttacking)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.30f;
                LockedOnLightAttack();
            }
        }

        if (isFreeLookActive && rightMouseClicked && !isHeavyAttacking)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.44f;
                FreeLookHeavyAttack();
            }
        }


        if (!isFreeLookActive && rightMouseClicked && !isHeavyAttacking)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.44f;
                LockedOnHeavyAttack();
            }
        }

        if (isFreeLookActive && spacePressed && !isDodging)
        {
            if(cooldownTimer <= 0)
            {
                cooldownTimer =1.37f;
                FreeLookDodge();
            }
        }

        if (!isFreeLookActive && spacePressed && !isDodging && forwardPressed && !rightPressed && !leftPressed)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnForwardDodge();
            }
        }


        if (!isFreeLookActive && spacePressed && !isDodging && leftPressed && !backPressed && !forwardPressed)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnLeftDodge();
            }
        }

        if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && !backPressed && !forwardPressed)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnRightDodge();
            }
        }

        if (!isFreeLookActive && spacePressed && !isDodging && backPressed && !rightPressed && !leftPressed)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnBackwardDodge();
            }
        }

        
        if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && rightPressed))
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnBackwardRightDiagonalRolling();
            }
        }
        

        if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && leftPressed))
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnBackwardLeftDiagonalRolling();
            }
        }

      
        if (!isFreeLookActive && spacePressed && !isDodging && (leftPressed && forwardPressed))
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnForwardLeftDiagonalRolling();
            }
        }

        if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && forwardPressed)
        {
            if (cooldownTimer <= 0)
            {
                cooldownTimer = 1.37f;
                LockedOnForwardRightDiagonalRolling();
            }
        }


        void FreeLookLightAttack()
        {
            // start freelook light attack
            if (isFreeLookActive && leftMouseClicked && !isLightAttacking)
            {
                isLightAttacking = true;
                animator.SetTrigger("isAttackingLight");

            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackingLight") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isLightAttacking = false;
            }
        }

        void LockedOnLightAttack()
        {
            // start locked on light attack
            if (!isFreeLookActive && leftMouseClicked && !isLightAttacking)
            {
                isLightAttacking = true;
                animator.SetTrigger("isAttackingLight");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackingLight") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isLightAttacking = false;
            }
        }

        void FreeLookHeavyAttack()
        {
            // start freelook heavy attack
            if (isFreeLookActive && rightMouseClicked && !isHeavyAttacking)
            {
                isHeavyAttacking = true;
                animator.SetTrigger("isAttackingHeavy");

            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackingHeavy") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isHeavyAttacking = false;
            }
        }

        void LockedOnHeavyAttack()
        {
            // start locked on heavy attack
            if (!isFreeLookActive && rightMouseClicked && !isHeavyAttacking)
            {
                isHeavyAttacking = true;
                animator.SetTrigger("isAttackingHeavy");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("AttackingHeavy") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isHeavyAttacking = false;
            }
        }

        void FreeLookDodge()
        {
            //start freelook rolling 
            if (isFreeLookActive && spacePressed && !isDodging)
            {
                
                animator.SetTrigger("isDodging");
                // player.rotationSpeed = 5f;      
                Debug.Log("is dodging true");
                isDodging = false;
                
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Dodging") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                //player.rotationSpeed = 10f;

                Debug.Log("is dodging false");
                isDodging = true;
            }
        }

        void LockedOnForwardDodge()
        {
            //start locked on forward rolling 
            if (!isFreeLookActive && spacePressed && !isDodging && forwardPressed && !rightPressed && !leftPressed)
            {
                isDodging = false;
                animator.SetTrigger("isDodging");
                
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Dodging") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                
                isDodging = true;
            }
        }

        void LockedOnLeftDodge()
        {
            //start locked on left rolling
            if (!isFreeLookActive && spacePressed && !isDodging && leftPressed && !backPressed && !forwardPressed)
            {
                isDodging = false;
                animator.SetTrigger("isDodgingLeft");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingLeft") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnRightDodge()
        {
            //start locked on right rolling
            if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && !backPressed && !forwardPressed)
            {
                isDodging = false;
                animator.SetTrigger("isDodgingRight");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingRight") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnBackwardDodge()
        {
            //start locked on backward rolling
            if (!isFreeLookActive && spacePressed && !isDodging && backPressed && !rightPressed && !leftPressed)
            {
                isDodging = false;
                animator.SetTrigger("isDodgingBackward");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingBackward") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnBackwardRightDiagonalRolling()
        {
            //start locked on backward right diagonal rolling
            if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && rightPressed))
            {
                isDodging = false;
                animator.SetTrigger("isDodgingBackwardRightDiagonal");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingBackwardRightDiagonal") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnBackwardLeftDiagonalRolling()
        {
            //start locked on backward left diagonal rolling
            if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && leftPressed))
            {
                isDodging = false;
                animator.SetTrigger("isDodgingBackwardLeftDiagonal");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingBackwardLeftDiagonal") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnForwardLeftDiagonalRolling()
        {
            //start locked on left forward diagonal rolling
            if (!isFreeLookActive && spacePressed && !isDodging && (leftPressed && forwardPressed))
            {
                isDodging = false;
                animator.SetTrigger("isDodgingLeftDiagonal");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingLeftDiagonal") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }

        void LockedOnForwardRightDiagonalRolling()
        {
            //start locked on right forward diagonal rolling
            if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && forwardPressed)
            {
                isDodging = false;
                animator.SetTrigger("isDodgingRightDiagonal");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DodgingRightDiagonal") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                isDodging = false;
            }
        }


        //start freelook walking
        if (isFreeLookActive && !spacePressed && !isWalking && (forwardPressed || rightPressed || leftPressed || backPressed))
        {
            animator.SetBool(isWalkingHash, true);
        }
        //stop freelook walking
        if (!isFreeLookActive || spacePressed || (isWalking && !(forwardPressed || rightPressed || leftPressed || backPressed)))
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


        //start locked on walk forward
        if (!isFreeLookActive && !isLockedOnWalkingForward && forwardPressed)
        {
            animator.SetBool(isLockedOnWalkingForwardHash, true);
        }

        //stop locked on walk forward
        if (isFreeLookActive || (isLockedOnWalkingForward && !forwardPressed))
        {
            animator.SetBool(isLockedOnWalkingForwardHash, false);
        }

        //start locked on walk backward
        if (!isFreeLookActive && !isLockedOnWalkingBackward && backPressed)
        {
            animator.SetBool(isLockedOnWalkingBackwardHash, true);
        }

        //stop locked on walk backward
        if (isFreeLookActive || (isLockedOnWalkingBackward && !backPressed))
        {
            animator.SetBool(isLockedOnWalkingBackwardHash, false);
        }


        //start locked on right strafe walking
        if (!isFreeLookActive && !isLockedOnWalkingRight && rightPressed)
        {
            animator.SetBool(isLockedOnWalkingRightHash, true);
        }

        //stop locked on right strafe walking
        if (isFreeLookActive || (isLockedOnWalkingRight && !rightPressed))
        {
            animator.SetBool(isLockedOnWalkingRightHash, false);
        }


        //start locked on left strafe walking
        if (!isFreeLookActive && !isLockedOnWalkingLeft && leftPressed)
        {
            animator.SetBool(isLockedOnWalkingLeftHash, true);
        }

        //stop locked on left strafe walking
        if (isFreeLookActive || (isLockedOnWalkingLeft && !leftPressed))
        {
            animator.SetBool(isLockedOnWalkingLeftHash, false);
        }


        //start locked on forward running
        if (!isFreeLookActive && !isLockedOnRunningForward && forwardPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRunningForwardHash, true);
        }

        //stop locked on forward running 
        if (isFreeLookActive || (isLockedOnRunningForward && (!forwardPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnRunningForwardHash, false);
        }


        //start locked on backward running 
        if (!isFreeLookActive && !isLockedOnRunningBackward && backPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRunningBackwardHash, true);
        }

        //stop locked on backward running 
        if (isFreeLookActive || (isLockedOnRunningBackward && (!backPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnRunningBackwardHash, false);
        }


        //start locked on right strafing
        if (!isFreeLookActive && !isLockedOnRightStrafing && rightPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnRightStrafingHash, true);
        }

        //stop locked on right strafing
        if (isFreeLookActive || (isLockedOnRightStrafing && (!rightPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnRightStrafingHash, false);
        }


        //start locked on left strafing
        if (!isFreeLookActive && !isLockedOnLeftStrafing && leftPressed && shiftPressed)
        {
            animator.SetBool(isLockedOnLeftStrafingHash, true);
        }

        //stop locked on left strafing
        if (isFreeLookActive || (isLockedOnLeftStrafing && (!leftPressed || !shiftPressed)))
        {
            animator.SetBool(isLockedOnLeftStrafingHash, false);
        }

    }
}
