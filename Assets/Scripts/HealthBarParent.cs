using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//INHERITANCE
public class HealthBarParent : MonoBehaviour
{
   [SerializeField] protected Slider slider;
   [SerializeField] protected Slider easeHealthSlider;
   [SerializeField] protected float lerpSpeed = 0.05f;

   [SerializeField] protected MonoBehaviour character;


    protected virtual void UpdateHealthSliders()
    {
        IHasHealth healthSource = character as IHasHealth;
        
            slider.value = healthSource.currentHealth;
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSource.currentHealth, lerpSpeed);
        
        
    }
}

