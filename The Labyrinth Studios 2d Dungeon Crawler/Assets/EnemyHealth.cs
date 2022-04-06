using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation if we have the hurt sprites

        if (Input.GetKeyDown("c"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        //Play die animation if we have the dying animation

        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;

        Destroy(gameObject);
    }


}
