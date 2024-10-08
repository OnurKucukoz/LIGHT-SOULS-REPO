using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    PlayerCombat player;
    public int damage = 20;
    bool isTriggered;
    Animator playerAnimator;
    AnimationStateController animationStateController;
    Player thePlayer;

    public bool isLightDamage;
    public bool isHeavyDamage;
    public bool isComboDamage;

    public int lightDamage = 10;
    public int heavyDamage = 20;
    public int comboDamage = 15;

    public AudioSource enemyWeaponAudioSource;
   

    public AudioClip heavyHitClip;
    public AudioClip playerGetsHeavyHit;
    public AudioClip playerGetsLightHit;
    public AudioClip enemyCutsPlayerLightHit;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        animationStateController = GameObject.Find("Player").GetComponent<AnimationStateController>();
        thePlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void LightDamage()
    {
        if (isTriggered )
        {
            playerAnimator.SetTrigger("isGettingHit");
            animationStateController.cooldownTimer = 0.41f;
            player.currentHealth -= lightDamage;
            StartCoroutine(StopRotationLight());
            enemyWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            enemyWeaponAudioSource.PlayOneShot(enemyCutsPlayerLightHit);
            enemyWeaponAudioSource.PlayOneShot(playerGetsLightHit);
            // set blood vfx
           
        }
    }

   
    public void HeavyDamage()
    {
        if (isTriggered)
        {
            playerAnimator.SetTrigger("isGettingHeavyHit");
            animationStateController.cooldownTimer = 1.71f;
            player.currentHealth -= heavyDamage;
            StartCoroutine(StopRotationHeavy());
            enemyWeaponAudioSource.pitch = 0.5f;
            enemyWeaponAudioSource.PlayOneShot(playerGetsHeavyHit);
            enemyWeaponAudioSource.pitch = Random.Range(1f, 1.2f);
            enemyWeaponAudioSource.PlayOneShot(heavyHitClip);
            
            // set blood vfx
        }

    }
    public void ComboDamage()
    {
        isComboDamage = true;

        if (isTriggered )
        {
            playerAnimator.SetTrigger("isGettingHit");
            animationStateController.cooldownTimer = 0.41f;
            player.currentHealth -= comboDamage;
            StartCoroutine(StopRotationLight());
            enemyWeaponAudioSource.pitch = Random.Range(1f, 1.2f);
            enemyWeaponAudioSource.PlayOneShot(enemyCutsPlayerLightHit);
            enemyWeaponAudioSource.PlayOneShot(playerGetsLightHit);

            // set blood vfx
            // set hit noise

        }

        isComboDamage = false;
    }
   IEnumerator StopRotationHeavy()
    {
        thePlayer.rotationSpeed = 0;
        yield return new WaitForSecondsRealtime(1.51f);
        thePlayer.rotationSpeed = 10;

    }

    IEnumerator StopRotationLight()
    {
        thePlayer.rotationSpeed = 0;
        yield return new WaitForSecondsRealtime(0.41f);
        thePlayer.rotationSpeed = 10;

    }

    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;

        if (other.transform.tag == "Player" && isLightDamage)
        {
            
            LightDamage();

        }

        if (other.transform.tag == "Player" && isHeavyDamage)
        {

            HeavyDamage();

        }

        if (other.transform.tag == "Player" && isComboDamage)
        {
            
            ComboDamage();

        }

        animationStateController.canHeavy = false;
        animationStateController.canLight = false;
        animationStateController.canHeal = false;
        animationStateController.canCombo = false;

        isTriggered = false;
        
    }
}
