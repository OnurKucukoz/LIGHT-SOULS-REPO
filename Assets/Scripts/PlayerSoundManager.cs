using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource playerAudioSource;

    public AudioClip playerFootstepClip;

    public AudioClip playerSwordSwoshSoundOne;

    public AudioClip playerSwordSwoshSoundHeavy;

    public AudioClip playerDeathSound;

    public AudioClip playerAttackYeahSound;

    public AudioClip playerAttackHuohSound;

    public AudioClip playerAttackCuohSound;

    public AudioClip playerBodyFallsDown;

    public AudioClip loseSound;

    public AudioClip medicineSound;



    public void PlayMedicineSound()
    {
        
        playerAudioSource.volume = 0.5f;
        playerAudioSource.PlayOneShot(medicineSound);
    }

    public void PlayLoseSound()
    {
        playerAudioSource.spatialBlend = 0f;
        playerAudioSource.volume = 0.5f;
        playerAudioSource.PlayOneShot(loseSound);
    }
    public void PlayPlayerBodyFallsDownSound()
    {
        playerAudioSource.pitch = Random.Range(1f, 1.2f);
        playerAudioSource.PlayOneShot(playerBodyFallsDown);
    }
   
    public void PlayPlayerAttackYeahSound()
    {
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerAttackYeahSound);
    }

    public void PlayPlayerAttackHuohSound()
    {
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerAttackHuohSound);
    }

    public void PlayPlayerAttackCuohSound()
    {
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerAttackCuohSound);
    }

    public void PlayPlayerDeathSound()
    {
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerDeathSound);
    }

    public void PlayPlayerHeavyAttackSound()
    {
        playerAudioSource.spatialBlend = 0.5f;
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerSwordSwoshSoundHeavy);
    }

    public void PlayPlayerLightAttackSound()
    {
        playerAudioSource.spatialBlend = 0.5f;
        playerAudioSource.pitch = Random.Range(0.8f, 1f);
        playerAudioSource.PlayOneShot(playerSwordSwoshSoundOne);
    }

}
