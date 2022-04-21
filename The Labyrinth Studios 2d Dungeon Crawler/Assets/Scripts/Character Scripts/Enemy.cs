using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**********************************************************************************************************************************************************************
//********************************************************ENEMY STATE MACHINE*******************************************************************************************
//**********************************************************************************************************************************************************************
public enum EnemyState
{
    idle, walk, attack, stagger, dead
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
    public float reviveRadius;//this will initiate the Revive routine
    public Vector2 homePosition;//the enemy's home position so it can return after chasing
    public float rangedRadius;
    public GameObject projectile;
    public float projectileForce;
    public float attCooldown = 2.5f;
    private float lastAttack = -9999f;
    //*******************************************************************************************************************************************************************
    //********************************************************INITIALIZATION*********************************************************************************************
    //*******************************************************************************************************************************************************************
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        thisBody = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy
        thrust = 5f;
        pushTime = .16f;        
    }

    // Update is called once per frame
    void Update()
    {
        awakeAndMove();
        if(Time.time > lastAttack +attCooldown)
        { RangedAttack(); lastAttack = Time.time; }
        
    }
   
    //*******************************************************************************************************************************************************************
    //*****************************************************************ENEMY MOVEMENT************************************************************************************
    //*******************************************************************************************************************************************************************
    void awakeAndMove()//Author Johnathan Bates
    {
        Vector2 temp;
        Vector2 tempDir;
        Vector2 castV3;
        //this checks to see if player is in chase range but outside of "attack range"
        //attack range is used so the enemy doesn't try to go through the player

        if (currentState != EnemyState.dead && Vector2.Distance(transform.position, target.position) <= chaseRadius
             && Vector2.Distance(transform.position, target.position) >= attackRadius)
        {
            
                //moves the enemy towards the player
                temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed);
                Move(thisBody, temp, moveSpeed);
                currentState = EnemyState.walk;
                tempDir = transform.position - target.position;
                MoveInDirection(tempDir);
            
        }
        else if (currentState != EnemyState.dead)
        {
            //makes the enemy return to it's home position
            temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed);
            Move(thisBody, temp, moveSpeed / 1.5f);
            castV3 = transform.position;
            tempDir = castV3 - homePosition;
            MoveInDirection(tempDir);
            currentState = EnemyState.walk;
            if (castV3 == homePosition)
            {
                anim.SetBool("moving", false);
                currentState = EnemyState.idle;
            }
        }
        
    }    
    void MoveInDirection(Vector2 tempDir)
    {
        anim.SetFloat("moveX", -tempDir.normalized.x);//allows movement animation
        anim.SetFloat("moveY", -tempDir.normalized.y);//allows movement animation
        anim.SetBool("moving", true);//moving set true in animator
    }
    //*******************************************************************************************************************************************************************
    //*****************************************************************RANGED ATTACK*************************************************************************************
    //*******************************************************************************************************************************************************************
    void RangedAttack()//Author Johnathan Bates
    {
        //This code looks like it could be condensed
        if (this.charType == CharacterType.rangedEnemy)
        {

            if (Vector2.Distance(transform.position, target.position) <= rangedRadius)//RayCast to check the line of sight to target
            {
                if ((Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .002f) || (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .002f))
                {
                    //This will fire a projectile along the x axis
                    if (transform.position.x < target.position.x && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .05f)
                        && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) > -.05f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileForce, 0f));
                    }
                    if (transform.position.x > target.position.x && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .05f)
                        && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) > -.05f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileForce, 0f));
                    }
                    //This will fire a projectile along the y axis
                    if (transform.position.y < target.position.y && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .05f)
                       && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) > -.05f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f,-90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, projectileForce));
                    }
                    if (transform.position.y > target.position.y && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .05f)
                        && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) > -.05f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f,90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -projectileForce));
                    }
                }
                

            }
        }
    }    
}
