using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;


public class AnimationStateController : MonoBehaviour
{
    Player player;
    CameraSwitcher cameraSwitcher;
    Animator animator;
    MeshCollider meshCollider;
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

    int isAttackingLight;
    int isAttackingHeavy;

    public bool isDodging;
    public float cooldownTimer = 0;

    public float comboTimer = 0;

    private bool isAttacking = false;
    public int comboStep = 0;

    public bool canCombo;
    public bool canHeavy;
    public bool canLight;
    public bool canHeal;
    public bool canStoneAttack;

    public float numOfClicks;
    public int medicineCount = 3;

    public float stamina;
    private float staminaCostLightAttack = 8;
    private float staminaCostHeavyAttack = 14;
    public float maxStamina = 140f;
    public bool isStaminaCostingNow;

    public TextMeshProUGUI medicineCountText;

   
    void Start()
    {
        Cursor.visible = false;
        playerLayer = LayerMask.NameToLayer("Player");
        invincibleLayer = LayerMask.NameToLayer("PlayerInvincible");
        meshCollider = GameObject.Find("Player").GetComponent<MeshCollider>();

        stamina = maxStamina;
        comboStep = 0;
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
        canCombo = false;
    }

    IEnumerator StaminaCostingNow()
    {
        if (isStaminaCostingNow) yield break; 

        isStaminaCostingNow = true;
        yield return new WaitForSeconds(3);
        isStaminaCostingNow = false;
    }


    int playerLayer;
    int invincibleLayer;
   

    public void EnableMeshCollider()
    {
        gameObject.layer = playerLayer;
    }

    public void DisableMeshCollider()
    {
        gameObject.layer = invincibleLayer;
    }

   
    public void EnableCombo()
    {
        canCombo = true;        
    }

    public void DisableCombo()
    {
        canCombo = false;      
    }

    public void EnableHeavy()
    {
        canHeavy = true;       
    }

    public void DisableHeavy()
    {
        canHeavy = false;      
    }

    public void EnableLight()
    {
        canLight = true;        
    }

    public void DisableLight()
    {
        canLight = false;       
    }

    public void EnableHeal()
    {
        canHeal = true;
    }

    public void DisableHeal()
    {
        canHeal = false;
    }

    void UpdateMedicineCount()
    {
        medicineCountText.text = (medicineCount-1).ToString();
    }
    
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Cursor.visible = false;
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
            bool rPressed = Input.GetKey("r");

            bool leftMouseClicked = Input.GetMouseButtonDown(0);
            bool rightMouseClicked = Input.GetMouseButtonDown(1);


            if (stamina <= 100 && !isStaminaCostingNow)
            {
                stamina += 15 * Time.deltaTime;
            }

            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }

            if (comboTimer > 0)
            {
                comboTimer -= Time.deltaTime;
            }

            if (numOfClicks >= 10)
            {
                cooldownTimer = -0.3f;
                numOfClicks = 0;
            }

            if (rPressed)
            {
                if (cooldownTimer <= 0 && medicineCount > 0 && !canHeavy && !canLight)
                {

                    cooldownTimer = 3.05f;
                    UseMedicineAnimation();
                    UpdateMedicineCount();

                }
            }

            if (leftMouseClicked)
            {
                numOfClicks++;
                if (cooldownTimer <= 0 && !isAttacking && !canHeavy && !canHeal && (stamina > staminaCostLightAttack))
                {
                    stamina -= 10;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.30f;
                    FreeLookLightAttack();

                }

            }

            if (rightMouseClicked)
            {
                numOfClicks++;
              
                if (cooldownTimer <= 0 && !canLight && !canHeal && (stamina > staminaCostHeavyAttack))
                {
                    stamina -= 20;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 2.2f;
                    FreeLookHeavyAttack();

                }
              
            }

            if (canCombo && leftMouseClicked)
            {

                if (!canHeavy && !canHeal && (stamina > 8))
                {
                    stamina -= 5;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.30f;
                    LightAttackComboTwo();
                    DisableCombo();

                }
            }

            if (isFreeLookActive && spacePressed && !isDodging)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    FreeLookDodge();
                }
            }

            if (!isFreeLookActive && spacePressed && !isDodging && forwardPressed && !rightPressed && !leftPressed)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnForwardDodge();
                }
            }


            if (!isFreeLookActive && spacePressed && !isDodging && leftPressed && !backPressed && !forwardPressed)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnLeftDodge();
                }
            }

            if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && !backPressed && !forwardPressed)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnRightDodge();
                }
            }

            if (!isFreeLookActive && spacePressed && !isDodging && backPressed && !rightPressed && !leftPressed)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnBackwardDodge();
                }
            }


            if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && rightPressed))
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnBackwardRightDiagonalRolling();
                }
            }


            if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && leftPressed))
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnBackwardLeftDiagonalRolling();
                }
            }


            if (!isFreeLookActive && spacePressed && !isDodging && (leftPressed && forwardPressed))
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnForwardLeftDiagonalRolling();
                }
            }

            if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && forwardPressed)
            {
                if (cooldownTimer <= 0 && (stamina > 10))
                {
                    stamina -= 7;
                    StartCoroutine(StaminaCostingNow());
                    cooldownTimer = 1.37f;
                    LockedOnForwardRightDiagonalRolling();
                }
            }

            void FreeLookDodge()
            {
                //start freelook rolling 

                animator.SetTrigger("isDodging");

                isDodging = false;

            }

            void LockedOnForwardDodge()
            {
                //start locked on forward rolling 
                if (!isFreeLookActive && spacePressed && !isDodging && forwardPressed && !rightPressed && !leftPressed)
                {
                    isDodging = false;
                    animator.SetTrigger("isDodging");

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

            }

            void LockedOnRightDodge()
            {
                //start locked on right rolling
                if (!isFreeLookActive && spacePressed && !isDodging && rightPressed && !backPressed && !forwardPressed)
                {
                    isDodging = false;
                    animator.SetTrigger("isDodgingRight");
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

            }

            void LockedOnBackwardRightDiagonalRolling()
            {
                //start locked on backward right diagonal rolling
                if (!isFreeLookActive && spacePressed && !isDodging && (backPressed && rightPressed))
                {
                    isDodging = false;
                    animator.SetTrigger("isDodgingBackwardRightDiagonal");
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

            }

            void LockedOnForwardLeftDiagonalRolling()
            {
                //start locked on left forward diagonal rolling
                if (!isFreeLookActive && spacePressed && !isDodging && (leftPressed && forwardPressed))
                {
                    isDodging = false;
                    animator.SetTrigger("isDodgingLeftDiagonal");
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

            // start freelook running
            if (isFreeLookActive && !isRunning && ((forwardPressed || rightPressed || leftPressed || backPressed) && shiftPressed) && stamina > 12)
            {
                animator.SetBool(isRunningHash, true);
                animator.SetBool(isWalkingHash, false);
            }

            else if (isFreeLookActive && !isRunning && ((forwardPressed || rightPressed || leftPressed || backPressed) && shiftPressed) && stamina < 10)
            {
                animator.SetBool(isRunningHash, false);
                animator.SetBool(isWalkingHash, true);
                player.speed = 1;
            }
            //--------------------------------------------

            //start locked on forward running
            if (!isFreeLookActive && !isLockedOnRunningForward && forwardPressed && shiftPressed && stamina > 12)
            {
                animator.SetBool(isLockedOnRunningForwardHash, true);
                animator.SetBool(isLockedOnWalkingForwardHash, false);
            }

            else if (!isFreeLookActive && !isLockedOnRunningForward && forwardPressed && shiftPressed && stamina < 10)
            {
                animator.SetBool(isLockedOnRunningForwardHash, false);
                animator.SetBool(isLockedOnWalkingForwardHash, true);
                player.speed = 1;
            }
            //--------------------------------------------
            //stop locked on forward running 
            if (isFreeLookActive || (isLockedOnRunningForward && (!forwardPressed || !shiftPressed)))
            {
                animator.SetBool(isLockedOnRunningForwardHash, false);

            }


            //start locked on backward running
            if (!isFreeLookActive && !isLockedOnRunningBackward && backPressed && shiftPressed && stamina > 12)
            {
                animator.SetBool(isLockedOnRunningBackwardHash, true);
                animator.SetBool(isLockedOnWalkingBackwardHash, false);
            }

            else if (!isFreeLookActive && !isLockedOnRunningBackward && backPressed && shiftPressed && stamina < 10)
            {
                animator.SetBool(isLockedOnRunningBackwardHash, false);
                animator.SetBool(isLockedOnWalkingBackwardHash, true);
                //player.speed = 1;
            }
            //--------------------------------------------
            //stop locked on backward running 
            if (isFreeLookActive || (isLockedOnRunningBackward && (!backPressed || !shiftPressed)))
            {
                animator.SetBool(isLockedOnRunningBackwardHash, false);
            }

            //start locked on right strafing
            if (!isFreeLookActive && !isLockedOnRightStrafing && rightPressed && shiftPressed && stamina > 12)
            {
                animator.SetBool(isLockedOnRightStrafingHash, true);
                animator.SetBool(isLockedOnWalkingRightHash, false);
            }

            else if (!isFreeLookActive && !isLockedOnRightStrafing && rightPressed && shiftPressed && stamina < 10)
            {
                animator.SetBool(isLockedOnRightStrafingHash, false);
                animator.SetBool(isLockedOnWalkingRightHash, true);
            }

            //--------------------------------------------
            //stop locked on right strafing
            if (isFreeLookActive || (isLockedOnRightStrafing && (!rightPressed || !shiftPressed)))
            {
                animator.SetBool(isLockedOnRightStrafingHash, false);
            }

            //start locked on leftt strafing
            if (!isFreeLookActive && !isLockedOnLeftStrafing && leftPressed && shiftPressed && stamina > 12)
            {
                animator.SetBool(isLockedOnLeftStrafingHash, true);
                animator.SetBool(isLockedOnWalkingLeftHash, false);
            }

            else if (!isFreeLookActive && !isLockedOnLeftStrafing && leftPressed && shiftPressed && stamina < 10)
            {
                animator.SetBool(isLockedOnLeftStrafingHash, false);
                animator.SetBool(isLockedOnWalkingLeftHash, true);
            }

            //stop locked on left strafing
            if (isFreeLookActive || (isLockedOnLeftStrafing && (!leftPressed || !shiftPressed)))
            {
                animator.SetBool(isLockedOnLeftStrafingHash, false);
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
        }
    }

    private void UseMedicineAnimation()
    {
        StartCoroutine(DeacreaseMedicineCountWithDelay());
        animator.SetTrigger("isHealing");
    }

    IEnumerator DeacreaseMedicineCountWithDelay()
    {
        yield return new WaitForSeconds(2.65f);
        medicineCount--;
    }

    void FreeLookHeavyAttack()
    {

        animator.SetTrigger("isAttackingHeavy");

    }

    void LightAttackComboTwo()
    {

        animator.SetTrigger("isLightComboTwo");

    }


    void FreeLookLightAttack()
    {

        animator.SetTrigger("isAttackingLight");

    }

}
