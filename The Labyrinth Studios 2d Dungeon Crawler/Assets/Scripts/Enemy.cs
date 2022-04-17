using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**********************************************************************************************************************************************************************
//********************************************************ENEMY STATE MACHINE*******************************************************************************************
//**********************************************************************************************************************************************************************
public enum EnemyState
{
    idle, walk, attack, stagger
}


public class Enemy : Character
{
    //*******************************************************************************************************************************************************************
    //********************************************************ENEMY CLASS ATTRIBUTES*************************************************************************************
    //*******************************************************************************************************************************************************************
    public EnemyState currentState;
    public string enemyName;
    public Transform target;//will be our player
    public float chaseRadius;//the radius in which the enemy will activate and chase the player
    public float attackRadius;//how far away we want the enemy to stop from the player
    public float knockBackRadius;//this will initiate the knockback routine
    public Vector2 homePosition;//the enemy's home position so it can return after chasing
    //*******************************************************************************************************************************************************************
    //********************************************************INITIALIZATION*********************************************************************************************
    //*******************************************************************************************************************************************************************
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        thisBody = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy
        currentHealth = 3;
        thrust = 5f;
        pushTime = .16f;
    }

    // Update is called once per frame
    void Update()
    {
        awakeAndMove();
    }
    //*******************************************************************************************************************************************************************
    //*****************************************************************ENEMY MOVEMENT************************************************************************************
    //*******************************************************************************************************************************************************************
    void awakeAndMove()
    {
        Vector2 temp;
        Vector2 tempDir;
        Vector2 castV3;
        //this checks to see if player is in chase range but outside of "attack range"
        //attack range is used so the enemy doesn't try to go through the player
        if (Vector2.Distance(transform.position, target.position) <= chaseRadius
             && Vector2.Distance(transform.position, target.position) >= attackRadius)
        {
            //moves the lint enemy towards the player
            temp = Vector2.MoveTowards(transform.position, target.position, Time.fixedDeltaTime);
            Move(thisBody, temp, moveSpeed);
            Debug.Log(thisBody.velocity);
            //*****************************************************************ENEMY ANIMATION**************************************************************************
            tempDir = transform.position - target.position;
            moveInDirection(tempDir);
        }
        else
        {
            //makes the lint enemy return to it's home position
            temp = Vector2.MoveTowards(transform.position, homePosition, Time.fixedDeltaTime);
            Move(thisBody, temp, moveSpeed / 1.5f);
            castV3 = transform.position;
            tempDir = castV3 - homePosition;
            moveInDirection(tempDir);
            if(castV3 == homePosition)
            {
                anim.SetBool("moving", false);
            }
        }
    }
    
    void moveInDirection(Vector2 tempDir)
    {
        anim.SetFloat("moveX", -tempDir.normalized.x);//allows movement animation
        anim.SetFloat("moveY", -tempDir.normalized.y);//allows movement animation
        anim.SetBool("moving", true);//moving set true in animator
    }
   
}
