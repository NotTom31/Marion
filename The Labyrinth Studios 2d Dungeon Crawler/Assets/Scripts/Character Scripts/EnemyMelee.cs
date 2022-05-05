using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy, IDamageable, IKillable, IMoveable
{
    // Start is called before the first frame update
    void Awake()
    {
        //-----------------------------
        /*Attributes from Character script*/
        //-----------------------------
        moveSpeed = .014f;
        charType = CharacterType.meleeEnemy;
        attackDamage = 1;
        currentHealth = 3;       
        //-----------------------------
        /*Attributes from Enemy script*/
        //-----------------------------
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy, this will be used to return to after chasing and for reviving
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component
        chaseRadius = 1.3f;
        attackRadius = .1f;
        reviveRadius = 5f;
    }
    private void Update()
    {
        Move();
    }
    public void Move()//Author Johnathan Bates
    {
        Vector2 temp;//Stores the vector that will pointing towards the player
        Vector2 tempDir;//Stores the vector that will make enemy face the player, also used in conjunction with castV3 to face home position
        Vector2 castV3;//Stores the vector that will make enemy face it's home position
        //this checks to see if player is in chase range but outside of "attack range", attack range is used so the enemy doesn't try to go through the player
        if (currentState != EnemyState.dead && Vector2.Distance(transform.position, target.position) <= chaseRadius
             && Vector2.Distance(transform.position, target.position) >= attackRadius)
        {//moves the enemy towards the player           
            currentState = EnemyState.attack;            
            tempDir = transform.position - target.position;
            MoveInDirection(tempDir);
            temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed);
            thisBody.MovePosition(temp);//moves the enemy
        }
        else if(currentState != EnemyState.dead)
        {//makes the enemy return to it's home position            
            currentState = EnemyState.walk;            
            castV3 = transform.position;
            tempDir = castV3 - homePosition;
            MoveInDirection(tempDir);
            temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed);
            thisBody.MovePosition(temp);//moves the enemy
            if (castV3 == homePosition)
            {
                anim.SetBool("moving", false);
                currentState = EnemyState.idle;
            }
        }
    }
    void MoveInDirection(Vector2 tempDir)//this will make the enemy face the direction it is moving in
    {
        anim.SetFloat("moveX", -tempDir.normalized.x);//allows movement animation
        anim.SetFloat("moveY", -tempDir.normalized.y);//allows movement animation
        if(currentState == EnemyState.walk)
        {
            anim.SetBool("attack", false);//attack set true in animator
            anim.SetBool("moving", true);//moving set true in animator
        }
        else if(currentState == EnemyState.attack)
        {
            anim.SetBool("moving", false);//moving set true in animator
            anim.SetBool("attack", true);//attack set true in animator
        }
        
    }
    //******************************************************************************************************************************************************
    //*****************************************************************MELEE ATTACKING TRIGGERED************************************************************
    //******************************************************************************************************************************************************
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))//check to make sure either player hits enemy or enemy hits player
        {
            if (obj.gameObject != null)
            {
                Damage(attackDamage, obj);
                Push(obj);
            }
        }
    }
}
