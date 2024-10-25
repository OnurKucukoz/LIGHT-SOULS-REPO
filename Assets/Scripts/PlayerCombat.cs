using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, IHasHealth
{
    public int currentHealth;
    int IHasHealth.currentHealth
    {
        get { return currentHealth; }
    }
    public int maxHealth = 100;
    
    Animator playerAnimator;
    Player player;
    AnimationStateController playerAnimationStateController;
    PlayerWeapon playerWeapon;
    MeshCollider playerMeshCol;
    Enemy enemy;
    LosePanel losePanel;

    
    void Start()
    {       
        player = GameObject.Find("Player").GetComponent<Player>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        playerAnimationStateController = GameObject.Find("Player").GetComponent<AnimationStateController>();
        playerWeapon = GameObject.Find("Attack Point").GetComponent<PlayerWeapon>();
        playerMeshCol = GameObject.Find("Player").GetComponent<MeshCollider>();
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
        losePanel = GameObject.Find("Lose Panel").GetComponent<LosePanel>();

        losePanel.losePanel.SetActive(false);
        losePanel.loseText.SetActive(false);
        currentHealth = maxHealth;
        TurnOffWeaponCollider();
    }

    private void Update()
    {        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void HealUp()
    {
        currentHealth += 60;
        currentHealth = Math.Min(currentHealth, 100);
    }

    public void TurnOnLightDamage()
    {
        playerWeapon.isLightDamage = true;
    }

    public void TurnOffLightDamage()
    {
        playerWeapon.isLightDamage = false;
    }
    public void TurnOnHeavyDamage()
    {
        playerWeapon.isHeavyDamage = true;
    }

    public void TurnOffHeavyDamage()
    {
        playerWeapon.isHeavyDamage = false;
    }

    public void PerformSecondLightAttack()
    {
        if (Input.GetMouseButtonDown(0))
        playerAnimator.SetTrigger("isLightComboTwo");   
    }

    public void TurnOnWeaponCollider()
    {
        playerWeapon.GetComponent<BoxCollider>().enabled = true;
    }

    public void TurnOffWeaponCollider()
    {
        playerWeapon.GetComponent<BoxCollider>().enabled = false;
    }

    public void Die()
    {
       
        player.enabled = false;
        playerAnimationStateController.enabled = false;
        playerMeshCol.enabled = false;
        
        playerAnimator.SetBool("isDead", true);
        enemy.enabled = false;
        StartCoroutine(ShowLosePanelAfterDelay());

        StartCoroutine(WaitForEndResetGame());

    }

    IEnumerator ShowLosePanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(4f);

        losePanel.ShowLosePanel();

    }

    IEnumerator WaitForEndResetGame()
    {
        yield return new WaitForSeconds(9.5f);
        SceneManager.LoadScene("SampleScene");
    }


}
