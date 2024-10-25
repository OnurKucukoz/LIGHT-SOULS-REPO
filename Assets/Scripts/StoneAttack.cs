using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAttack : MonoBehaviour
{
    PlayerCombat player;
    Animator playerAnimator;
    AnimationStateController animationStateController;
    Player thePlayer;

    bool isTriggered;

    public AudioSource enemyWeaponAudioSource;

    public int stoneDamage = 30;
    public AudioClip playerGetsHeavyHit;
    public AudioClip stoneHitClip;

    public GameObject bloodEffectPrefab;
    public ParticleSystem bloodEffectVFX;
    public Vector3 hitPoint;


    public bool isStoneDamage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        animationStateController = GameObject.Find("Player").GetComponent<AnimationStateController>();
        thePlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void StoneDamage()
    {
        if (isTriggered)
        {
            playerAnimator.SetTrigger("isGettingHeavyHit");
            animationStateController.cooldownTimer = 1.71f;
            player.currentHealth -= stoneDamage;
            StartCoroutine(StopMovementHeavy());
            StartCoroutine(StopRotationHeavy());
            enemyWeaponAudioSource.pitch = 0.5f;
            enemyWeaponAudioSource.PlayOneShot(playerGetsHeavyHit);
            enemyWeaponAudioSource.pitch = Random.Range(0.8f, 1f);
            enemyWeaponAudioSource.PlayOneShot(stoneHitClip);

        }

    }

    IEnumerator StopRotationHeavy()
    {
        thePlayer.rotationSpeed = 0;
        yield return new WaitForSecondsRealtime(1.51f);
        thePlayer.rotationSpeed = 10;

    }

    IEnumerator StopMovementHeavy()
    {
        thePlayer.speed = 0;
        thePlayer.isFallenDown = true;
        yield return new WaitForSeconds(1.51f);
        thePlayer.isFallenDown = false;

    }
    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;


        if (other.transform.tag == "Player" && isStoneDamage)
        {
            hitPoint = other.transform.position;
            Instantiate(bloodEffectVFX, hitPoint, other.transform.rotation);
            bloodEffectVFX.Play();
            StoneDamage();
        }

        animationStateController.canHeavy = false;
        animationStateController.canLight = false;
        animationStateController.canHeal = false;
        animationStateController.canCombo = false;
        animationStateController.canStoneAttack = false;

        isTriggered = false;

    }
}
