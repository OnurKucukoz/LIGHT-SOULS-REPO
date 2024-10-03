using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    Enemy enemy;

    public int damage = 50;

    bool isTriggered;

    public bool isLightDamage;
    public bool isHeavyDamage;
    public bool isComboDamage;

    public int lightDamage = 10;
    public int heavyDamage = 30;
    

    Animator enemyAnimator;

    private void Start()
    {
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
        enemyAnimator = GameObject.Find("newBosss").GetComponent<Animator>();

    }

    public void Damage(int damage)
    {
        if (isTriggered )
        {
            enemy.currentHealth -= damage;
            // set blood vfx
            // set hit noise
            // animasyon belli bir yerdeyken vurabilsin sadece
        }
        else Debug.Log("Not happended");
    }

    public void LightDamage()
    {


        if (isTriggered)
        {
            
            enemy.currentHealth -= lightDamage;
            // set blood vfx
            // set hit noise

        }


    }

    public void HeavyDamage()
    {

        if (isTriggered)
        {

            enemy.currentHealth -= heavyDamage;
            // set blood vfx
            // set hit noise

        }


    }

    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
       

        if (other.transform.tag == "Enemy" && isLightDamage)
        {

            LightDamage();

        }

        if (other.transform.tag == "Enemy" && isHeavyDamage)
        {

            HeavyDamage();

        }

        enemy.TurnOffWeaponCollider();
        isTriggered = false;
    }
}