using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerCombat : MonoBehaviour
{
    
    
    public int currentHealth;
    public int maxHealth = 100;
    
    Animator playerAnimator;
    Player player;
    AnimationStateController playerAnimationStateController;
    PlayerWeapon playerWeapon;

    private bool isAttacking = false;
    private int comboStep = 0; 
    private float lastAttackTime = 0f; 
    public float comboResetTime = 1f; 


    public Collider other;
    bool isCollided;

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

       

        TurnOffWeaponCollider();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
/*
        if (UnityEngine.Input.GetMouseButtonDown(0) && !isAttacking) 
        {
            PerformCombo();
        }*/

        
     /*   if (Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }*/
    }

    void PerformCombo()
    {
        lastAttackTime = Time.time;
        isAttacking = true;

        if (comboStep == 0)
        {
            playerAnimator.SetTrigger("isAttackingLight");
            comboStep = 1;
            StartCoroutine(ResetAttackFlag());
        }
        else if (comboStep == 1)
        {
            playerAnimator.SetTrigger("isLightComboTwo");
            comboStep = 2;
            StartCoroutine(ResetAttackFlag());
        }
        else if (comboStep == 2)
        {
            playerAnimator.SetTrigger("isLightComboLast");
            comboStep = 0;
            StartCoroutine(ResetAttackFlag());
        }

        //StartCoroutine(ResetAttackFlag());
    }

    IEnumerator ResetAttackFlag()
    {
        AnimatorStateInfo animStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        
        yield return new WaitForSeconds(animStateInfo.length * 0.3f); 

        isAttacking = false; 
    }

    void ResetCombo()
    {
        comboStep = 0;
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
        playerAnimator.SetBool("isDead", true);

    }




}
