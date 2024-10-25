using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public AudioClip heavyAttackGrunt;
    public AudioSource enemyAudioSource;
    public AudioClip lightAttackGrunt;
    public AudioClip axeSwooshSound;
    public AudioClip enemyFootsteps;
    public AudioClip victorySound;
    public AudioClip stoneShatterSound;

    public void PlayVictorySound()
    {
        enemyAudioSource.spatialBlend = 0f;
        enemyAudioSource.volume = 0.5f;
        enemyAudioSource.PlayOneShot(victorySound);
    }
    public void PlayFootstepsSound()
    {
        enemyAudioSource.spatialBlend = 1f;
        enemyAudioSource.pitch = Random.Range(0.8f, 1f);
        enemyAudioSource.PlayOneShot(enemyFootsteps);
    }

    public void PlayWushSoundAxe()
    {
        enemyAudioSource.pitch = Random.Range(0.8f, 1f);
        enemyAudioSource.PlayOneShot(axeSwooshSound);
    }
    public void PlayLightGruntSound()
    {
        enemyAudioSource.pitch = Random.Range(0.8f, 1f);
        enemyAudioSource.PlayOneShot(lightAttackGrunt);
    }
    public void PlayHeavyGruntSound()
    {
        enemyAudioSource.PlayOneShot(heavyAttackGrunt);
    }

    public void PlayStoneShatterSound()
    {
        enemyAudioSource.PlayOneShot(stoneShatterSound);
    }
}
