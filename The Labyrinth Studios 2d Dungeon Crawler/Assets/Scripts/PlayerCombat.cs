using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

    }

    void Attack()
    { 
        //play attack animation
        animator.SetTrigger("Attack");

        //detect enemies in range of attack
        //damage them
    }
    
}
