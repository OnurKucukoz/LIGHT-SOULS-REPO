using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : HealthBarParent  //INHERITANCE
{
    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            UpdateHealthSliders();
        }
    }
}
