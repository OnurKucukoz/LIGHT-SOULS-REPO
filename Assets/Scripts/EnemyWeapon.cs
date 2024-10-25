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

    public int lightDamage = 8;
    public int heavyDamage = 15;
    public int comboDamage = 12;

    public AudioSource enemyWeaponAudioSource;
   

    public AudioClip heavyHitClip;
    public AudioClip playerGetsHeavyHit;
    public AudioClip playerGetsLightHit;
    public AudioClip enemyCutsPlayerLightHit;

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
            StartCoroutine(StopMovementHeavy());
            enemyWeaponAudioSource.pitch = 0.5f;
            enemyWeaponAudioSource.PlayOneShot(playerGetsHeavyHit);
            enemyWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            enemyWeaponAudioSource.PlayOneShot(heavyHitClip);
            
        }

    }
    IEnumerator StopMovementHeavy()
    {
        thePlayer.speed = 0;
        thePlayer.isFallenDown = true;
        yield return new WaitForSeconds(1.51f);
        thePlayer.isFallenDown = false;

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
            enemyWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            enemyWeaponAudioSource.PlayOneShot(enemyCutsPlayerLightHit);
            enemyWeaponAudioSource.PlayOneShot(playerGetsLightHit);

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

    public GameObject bloodEffectPrefab;
    public ParticleSystem bloodEffectVFX;
    public Vector3 hitPoint;
    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;

        if (other.transform.tag == "Player" && isLightDamage)
        {
            hitPoint = other.transform.position;          
            Instantiate(bloodEffectVFX, hitPoint, other.transform.rotation);   
            bloodEffectVFX.Play();
            LightDamage();

        }

        if (other.transform.tag == "Player" && isHeavyDamage)
        {
            hitPoint = other.transform.position;
            Instantiate(bloodEffectVFX, hitPoint, other.transform.rotation);
            bloodEffectVFX.Play();
            HeavyDamage();

        }

        if (other.transform.tag == "Player" && isComboDamage)
        {
            hitPoint = other.transform.position;
            Instantiate(bloodEffectVFX, hitPoint, other.transform.rotation);            
            bloodEffectVFX.Play();
            ComboDamage();
        }

        animationStateController.canHeavy = false;
        animationStateController.canLight = false;
        animationStateController.canHeal = false;
        animationStateController.canCombo = false;

        isTriggered = false;
        
    }
}
