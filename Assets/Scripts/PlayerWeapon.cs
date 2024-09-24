using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    Enemy enemy;

    public int damage = 50;

    bool isTriggered;

    private void Start()
    {
        enemy = GameObject.Find("newBosss").GetComponent<Enemy>();
    }

    public void Damage(int damage)
    {
        if (isTriggered && enemy.transform.tag == "Enemy")
        {
            enemy.currentHealth -= damage;
            // set blood vfx
            // set hit noise
            // animasyon belli bir yerdeyken vurabilsin sadece
        }
        else Debug.Log("Not happended");
    }

    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (other.transform.tag == "Enemy")
        {
            Damage(12);
        }
        isTriggered = false;
    }
}