using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    PlayerCombat player;
    public int damage = 20;
    bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }


    public void Damage()
    {
        if (isTriggered && player.transform.tag == "Player")
        {
            player.currentHealth -= damage;
            // set blood vfx
            // set hit noise
            // animasyon belli bir yerdeyken vurabilsin sadece
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (other.transform.tag == "Player")
        {

            Damage();

        }
        isTriggered = false;
    }
}
