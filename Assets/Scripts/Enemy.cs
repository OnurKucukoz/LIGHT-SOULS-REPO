using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour, IHasHealth
{
    public int currentHealth;
    //ENCAPSULATION
    int IHasHealth.currentHealth
    {
        get { return currentHealth; }
    }

    private int maxHealth = 200;
    Enemy enemy;
    Animator enemyAnimator;
    EnemyWeapon enemyWeapon;
    StoneAttack stoneAttack;
    Rigidbody enemyRigidbody;
    VictoryPanel victoryPanel;
   

    bool isLightAttacking;
    bool isHeavyAttacking;
    float rotationSpeed;
    float distanceToPlayer;
    public GameObject player;
    public float moveSpeed = 4f;
    public float followRange = 15000f;

    public AudioSource enemyWeaponAudioSource;
    public AudioClip enemyDyingSound;
    
    public float attackRange = 1.5f; 
    
    public float attackCooldown = 5f;
    public bool canAttack = true;

    public BoxCollider stoneAttackCollider;
    public ParticleSystem stoneAttackVFX;

    public Vector3 targetColliderSize; 
    public float growthDuration = 1f;
    public Vector3 initialColliderSize = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 colliderCenterOffset = new Vector3(0f, 0f, 1f);

    public void EnableStoneAttack()
    {

        stoneAttackCollider.size = initialColliderSize;
        stoneAttackCollider.center = colliderCenterOffset;

        stoneAttackCollider.enabled = true;
        stoneAttackVFX.transform.rotation = transform.rotation;
        stoneAttackVFX.transform.position = transform.position;
        stoneAttackVFX.Play();
       
        StartCoroutine(GrowStoneAttack());

    }
    private IEnumerator GrowStoneAttack()
    {
       
        Vector3 initialSize = stoneAttackCollider.size;

        
        float elapsedTime = 0f;
        while (elapsedTime < growthDuration)
        {
            
            float t = elapsedTime / growthDuration;

            
            stoneAttackCollider.size = Vector3.Lerp(initialSize, targetColliderSize, t);

            
            elapsedTime += Time.deltaTime;

           
            yield return null;
        }

        
        stoneAttackCollider.size = targetColliderSize;

        stoneAttackCollider.enabled = false;
    }

    public void DisableStoneAttack()
    {
        stoneAttackCollider.enabled = false;
    }
    public void TurnOnWeaponCollider()
    {
        enemyWeapon.GetComponent<BoxCollider>().enabled = true;
    }

    public void TurnOffWeaponCollider()
    {
        enemyWeapon.GetComponent<BoxCollider>().enabled = false;
    }

    public void TurnOnLightDamage()
    {
        enemyWeapon.isLightDamage = true;
    }

    public void TurnOffLightDamage()
    {
        enemyWeapon.isLightDamage = false;
    }
    public void TurnOnHeavyDamage()
    {
        enemyWeapon.isHeavyDamage = true;
    }

    public void TurnOffHeavyDamage()
    {
        enemyWeapon.isHeavyDamage = false;
    }
    public void TurnOnComboDamage()
    {
        enemyWeapon.isComboDamage = true;
    }

    public void TurnOffComboDamage()
    {
        enemyWeapon.isComboDamage = false;
    }

    public void TurnOnStoneDamage()
    {
        stoneAttack.isStoneDamage = true;
    }

    public void TurnOffStoneDamage()
    {
        stoneAttack.isStoneDamage = false;
    }

    void Start()
    {
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
        enemyAnimator = GameObject.Find("newBosss").GetComponent<Animator>();
        enemyWeapon = GameObject.Find("EnemyWeaponCol").GetComponent<EnemyWeapon>();
        enemyRigidbody = GameObject.Find("newBosss").GetComponent<Rigidbody>();
        victoryPanel = GameObject.Find("Victory Panel").GetComponent<VictoryPanel>();
        stoneAttackCollider = GameObject.Find("StoneAttackSpell").GetComponent<BoxCollider>();
        stoneAttack = GameObject.Find("StoneAttackSpell").GetComponent<StoneAttack>();
        

        currentHealth = maxHealth;
        victoryPanel.victoryPanel.SetActive(false);
        victoryPanel.victoryAchievedText.SetActive(false);
        TurnOffWeaponCollider();
        isDodging = false;
    }

    private void Update()
    {
        
        if (currentHealth <= 0)
        {
            Die();
        }
        distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
        Vector3 directionToTarget = player.transform.position - transform.position;
        directionToTarget.y = 0;
        rotationSpeed = 5f;

        if (directionToTarget.magnitude > 1f) // Avoid rotating if too close
        {
            Quaternion toRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (distanceToPlayer <= followRange)
        {
            
            if (distanceToPlayer > attackRange && !isDodging )
            {
                FollowPlayer();
            }
            
            else if (distanceToPlayer <= attackRange && canAttack && !isDodging)
            {
                LookAtPlayer();
                AttackPlayer();
                StopMoving();

            }
           if(distanceToPlayer <= 2.4f)
            {
                StopMoving();
            }           
        }
        else
        {
            enemyAnimator.SetBool("isEnemyMidRunning", false);
        }

        
    }

    private void StopMoving()
    {     
        enemyAnimator.SetBool("isEnemyMidRunning", false);
    }

    public void Die()
    {
        moveSpeed = 0f;
        enemy.enabled = false;

        enemyAnimator.SetBool("isEnemyDead",true);
        enemyWeaponAudioSource.PlayOneShot(enemyDyingSound);
        StartCoroutine(ShowVictoryPanelAfterDelay());
        StartCoroutine(GoToCreditsScene());
       
    }

    IEnumerator GoToCreditsScene()
    {
        yield return new WaitForSeconds(9.5f);
        SceneManager.LoadScene("CreditsScene");
        Cursor.visible = true;
    }

    IEnumerator ShowVictoryPanelAfterDelay()
    {       
        yield return new WaitForSecondsRealtime(4f);

        victoryPanel.ShowVictoryPanel();
        
    }

    public void EndingEnemyLife()
    {
       enemyAnimator.enabled = false;
       enemyRigidbody.constraints = RigidbodyConstraints.FreezeAll;

    }
    void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        transform.position += direction * moveSpeed * Time.deltaTime;

        enemyAnimator.SetBool("isEnemyMidRunning", true);
    }

    bool isDodging;
    

    public void AttackPlayer()
    {
        
        int randomAttack = UnityEngine.Random.Range(0, 4); 
    
        switch (randomAttack )
        {
            case 0:
                StartCoroutine(StoneAttack());
                break;
            case 1:
                StartCoroutine(LightAttack());
                break;
            case 2:
                StartCoroutine(HeavyAttack());              
                break;
            case 3:
                StartCoroutine(ComboAttack());
                break;

        }      
        canAttack = false;
    }

    IEnumerator StoneAttack()
    {
        isDodging = true;
        enemyAnimator.SetTrigger("isEnemyStoneAttacking");
        yield return new WaitForSecondsRealtime(3.24f);
        canAttack = true;
        RandomDodgeMoves();
        isDodging = false;
    }
    IEnumerator ComboAttack()
    {
        isDodging = true;
        enemyAnimator.SetTrigger("isEnemyCombo");
        yield return new WaitForSecondsRealtime(5.24f);
        canAttack = true;
        RandomDodgeMoves();
        isDodging = false;
    }

    IEnumerator LightAttack()
    {
        isDodging = true;
        enemyAnimator.SetTrigger("isEnemyLightAttacking");
        yield return new WaitForSecondsRealtime(3.28f);
        canAttack = true;
        RandomDodgeMoves(); 
        isDodging = false;
        
    }
    IEnumerator HeavyAttack()
    {
        isDodging = true;
        enemyAnimator.SetTrigger("isEnemyHeavyAttacking");

        yield return new WaitForSecondsRealtime(4.20f);
        canAttack = true;
        RandomDodgeMoves();
        isDodging = false;
    }

    
    void RandomDodgeMoves()
    {
        int randomDodgeMoves = UnityEngine.Random.Range(0, 5);

        switch (randomDodgeMoves)
        {
            case 0:
               StartCoroutine(WalkBackward());   
                break;
            case 1:
               StartCoroutine(DodgeRight());
                break;
            case 2:
               StartCoroutine(DodgeLeft());
                break;
            case 3:
               StartCoroutine(DodgeBackward());
                
                break;
        }
    }


    private void JumpAttack()
    {
        enemyAnimator.SetTrigger("isEnemyJumpAttacking");
    }

    IEnumerator DodgeBackward()
    {
        isDodging = true;

        enemyAnimator.SetTrigger("isEnemyDodgingBackward");
        moveSpeed = 0;
        yield return new WaitForSecondsRealtime(3f);
        moveSpeed = 4f;
        isDodging = false;



    }
 
    void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500f);
    }
    IEnumerator DodgeRight()
    {
        isDodging = true;

        moveSpeed = 0;
        enemyAnimator.SetTrigger("isEnemyDodgingRight");
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 


        yield return new WaitForSecondsRealtime(2f);
        moveSpeed = 4f;
        isDodging = false;
    }

    IEnumerator DodgeLeft()
    {
        isDodging = true;

        moveSpeed = 0;
        enemyAnimator.SetTrigger("isEnemyDodgingLeft");
        yield return new WaitForSecondsRealtime(2f);
        moveSpeed = 4f;
        isDodging = false;
    }

    IEnumerator WalkBackward()
    {
        isDodging = true;

        enemyAnimator.SetBool("isEnemyWalkingBack",true);
        
        yield return new WaitForSecondsRealtime(1.20f);
        enemyAnimator.SetBool("isEnemyWalkingBack", false);
       
        isDodging = false;

    }
}
