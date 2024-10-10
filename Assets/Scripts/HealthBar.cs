using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthBarParent //INHERITANCE
{
    private PlayerCombat playerCombat;
   

    private void Start()
    {     
        slider = GameObject.Find("Player Health Bar").GetComponent<Slider>();
        easeHealthSlider = GameObject.Find("Player Ease Health Bar").GetComponent<Slider>();

        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    private void Update()
    {        
            UpdateHealthSliders();   
    }

    protected override void UpdateHealthSliders() //POLYMORPHISM
    {       
            slider.value = playerCombat.currentHealth;
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerCombat.currentHealth, lerpSpeed);
    }
}
