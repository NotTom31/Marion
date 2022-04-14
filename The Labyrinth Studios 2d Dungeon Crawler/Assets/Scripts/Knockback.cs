using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fighter"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();//initializes code with the enemy
            if (enemy != null)//if enemy is still alive
            {

                Vector2 difference = enemy.transform.position - transform.position;//causes the knockback of enemy
                difference = difference.normalized * thrust;//controls the force of the knockback
                enemy.AddForce(difference, ForceMode2D.Impulse);//is the force of the knockback
                StartCoroutine(KnockCo(enemy));//see below
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)//if enemy is still alive
        {
            yield return new WaitForSeconds(knockTime);//how long the knockback happens
           
            enemy.velocity = Vector2.zero;//stops the knockback
        }
    }

}
