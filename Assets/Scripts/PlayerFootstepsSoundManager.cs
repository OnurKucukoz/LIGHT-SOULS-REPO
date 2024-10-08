using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsSoundManager : MonoBehaviour
{
    public AudioSource playerFootstepsAudioSource;

    public AudioClip playerFootstepClip;

    public AudioClip playerRollSound;
    public void PlayPlayerFootstepClip()
    {
        playerFootstepsAudioSource.spatialBlend = 0.5f;
        playerFootstepsAudioSource.volume = 0.5f;
        playerFootstepsAudioSource.pitch = Random.Range(1f, 1.2f);
        playerFootstepsAudioSource.PlayOneShot(playerFootstepClip);
    }

    public void PlayPlayerRollSound()
    {
        playerFootstepsAudioSource.volume = 0.5f;
        playerFootstepsAudioSource.spatialBlend = 0.9f;
        playerFootstepsAudioSource.pitch = Random.Range(1f, 1.2f);
        playerFootstepsAudioSource.PlayOneShot(playerRollSound);
    }

}
