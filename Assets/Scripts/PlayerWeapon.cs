using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    Enemy enemy;

    bool isTriggered;

    public bool isLightDamage;
    public bool isHeavyDamage;
    public bool isComboDamage;

    public int lightDamage = 10;
    public int heavyDamage = 2;

    public AudioSource playerWeaponAudioSource;

    public AudioClip playerLightCutsEnemy;
    public AudioClip playerHeavyCutsEnemy;
    

    private void Start()
    {
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
    }

    public void LightDamage()
    {

        if (isTriggered)
        {  
            enemy.currentHealth -= lightDamage;
            playerWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            playerWeaponAudioSource.PlayOneShot(playerLightCutsEnemy);
            // set blood vfx
        }
    }

    public void HeavyDamage()
    {

        if (isTriggered)
        {
            enemy.currentHealth -= heavyDamage;
            playerWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            playerWeaponAudioSource.PlayOneShot(playerHeavyCutsEnemy);

            // set blood vfx
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