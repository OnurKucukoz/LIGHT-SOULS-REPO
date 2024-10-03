using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    Enemy enemy;
    public Slider slider;
    public Gradient gradient;
    private float lerpSpeed = 0.05f;

    public Slider easeHealthSlider;
    public Image fill;

    private void Start()
    {
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
        slider = GameObject.Find("Enemy Health Bar").GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = enemy.currentHealth;

        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, enemy.currentHealth, lerpSpeed);

        // fill.color = gradient.Evaluate(1f);

        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
