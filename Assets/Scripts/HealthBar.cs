using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerCombat playerCombat;
    public Slider slider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;
    public Image fill;

    private void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        slider = GameObject.Find("Player Health Bar").GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = playerCombat.currentHealth;

        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerCombat.currentHealth, lerpSpeed);

    }
}
