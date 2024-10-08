using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    AnimationStateController animationStateController;
    public Slider slider;
    public Slider easeStaminaSlider;
    private float lerpSpeed = 0.05f;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        animationStateController = GameObject.Find("Player").GetComponent<AnimationStateController>();
        slider = GameObject.Find("Stamina Bar").GetComponent<Slider>();
    }


 
    void Update()
    {
        slider.value = animationStateController.stamina;

        easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, animationStateController.stamina, lerpSpeed);

    }
}
