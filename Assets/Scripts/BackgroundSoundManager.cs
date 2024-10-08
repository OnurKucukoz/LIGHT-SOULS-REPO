using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    private PlayerCombat playerHealth;
    private Enemy enemyHealth;

    public AudioSource backgroundAudioSource;
    public AudioClip victorySound;
    public AudioClip loseSound;
    public AudioClip bossFightTheme;

    public float fadeSpeed = 0.5f; 
   
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerCombat>();
        enemyHealth = GameObject.Find("newBosss").GetComponent<Enemy>();
        backgroundAudioSource = GameObject.Find("New Background Sound Manager").GetComponent<AudioSource>();

        backgroundAudioSource.clip = bossFightTheme; 
        backgroundAudioSource.loop = true; 
        backgroundAudioSource.Play();
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0 || enemyHealth.currentHealth <= 0)
        {
            backgroundAudioSource.volume = Mathf.Lerp(backgroundAudioSource.volume, 0, fadeSpeed * Time.deltaTime);
   
            if (backgroundAudioSource.volume <= 0.01f)
            {
                backgroundAudioSource.volume = 0; 
                backgroundAudioSource.Stop();
            }
        }
    }
}