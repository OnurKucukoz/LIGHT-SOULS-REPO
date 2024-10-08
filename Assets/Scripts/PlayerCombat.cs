using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   
    public int currentHealth;
    public int maxHealth = 100;
    
    Animator playerAnimator;
    Player player;
    AnimationStateController playerAnimationStateController;
    PlayerWeapon playerWeapon;
    MeshCollider playerMeshCol;
    Enemy enemy;

     
    public float comboResetTime = 1f; 


    public Collider other;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {       
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerAnimationStateController = GameObject.Find("Player").GetComponent<AnimationStateController>();
        playerWeapon = GameObject.Find("Attack Point").GetComponent<PlayerWeapon>();
        playerMeshCol = GameObject.Find("Player").GetComponent<MeshCollider>();
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();


        TurnOffWeaponCollider();
    }

    private void Update()
    {        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void HealUp()
    {
        currentHealth += 30;
        currentHealth = Math.Min(currentHealth, 100);
    }

    public void TurnOnLightDamage()
    {
        playerWeapon.isLightDamage = true;
    }

    public void TurnOffLightDamage()
    {
        playerWeapon.isLightDamage = false;
    }
    public void TurnOnHeavyDamage()
    {
        playerWeapon.isHeavyDamage = true;
    }

    public void TurnOffHeavyDamage()
    {
        playerWeapon.isHeavyDamage = false;
    }

    public void PerformSecondLightAttack()
    {
       

        if (UnityEngine.Input.GetMouseButtonDown(0))
        playerAnimator.SetTrigger("isLightComboTwo");

        
    }

    public void TurnOnWeaponCollider()
    {
        playerWeapon.GetComponent<BoxCollider>().enabled = true;
    }

    public void TurnOffWeaponCollider()
    {
        playerWeapon.GetComponent<BoxCollider>().enabled = false;
    }

    public void Die()
    {
       
        player.enabled = false;
        playerAnimationStateController.enabled = false;
        playerMeshCol.enabled = false;
        
        playerAnimator.SetBool("isDead", true);
        enemy.enabled = false;

        // lose screen
        // lose music 7 sec
    }




}
