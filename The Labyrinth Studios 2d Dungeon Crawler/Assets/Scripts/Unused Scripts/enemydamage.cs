using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydamage : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    float attackCD = 5f;
    int damage = 1;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if(attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
    }

    void Attack()
    {

        attackCD = 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided");
            collision.gameObject.GetComponent<playermovement>().TakeDamage(damage);
        }
    }
}

