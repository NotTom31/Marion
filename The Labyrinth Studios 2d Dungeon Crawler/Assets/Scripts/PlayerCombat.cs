using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public Transform attackPointDown;
    public Transform attackPointUp;
    public Transform aaaaaaaaaa;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
           Attack();
        }
        //the code below checks to play the correct attack animation based off of last input
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastAttackHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastAttackVertical", Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            attackPoint.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            attackPoint.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            attackPoint.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            attackPoint.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate()
    {

        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fighter"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Debug.Log("Enemy hit!");
        }
    }

    void Attack()
    { 
        //play attack animation
        animator.SetBool("Attack", true);      
      
       
        //detect enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage them
        /* foreach (Collider2D enemy in hitEnemies)
         {
             Debug.Log("We hit" + enemy.name);
             enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
         }*/



    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
