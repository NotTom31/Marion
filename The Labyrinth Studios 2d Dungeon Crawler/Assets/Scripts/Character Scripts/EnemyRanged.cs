using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy, IDamageable, IKillable, IMoveable, IPushable
{
    private float rangedRadius;//This will be used for the ranged enemy attack radius
    public GameObject projectile;//this will be created by the ranged enemy as a projectile
    private float projectileForce;//this will be used to determine how fast the projectile will travel
    private float attCooldown;//this will be used in conjunction with lastAttack to control how fast the ranged enemy attacks
    private float lastAttack = -9999f;//this will be used in conjunction with attCooldown to control how fast the ranged enemy attacks
    void Awake()
    {
        moveSpeed = .005f;
        charType = CharacterType.rangedEnemy;
        attackDamage = 1;
        currentHealth = 3;
        chaseRadius = 4.5f;
        attackRadius = .8f;
        reviveRadius = 5f;
        rangedRadius = 4;
        projectileForce = 80;
        attCooldown = 1;
        //-----------------------------
        /*Attributes from Enemy script*/
        //-----------------------------
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy, this will be used to return to after chasing and for reviving
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component
    }
    void Update()
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
             && Vector2.Distance(transform.position, target.position) > attackRadius)
        {//moves the enemy towards the player
            temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed);
            thisBody.MovePosition(temp);//moves the enemy
            currentState = EnemyState.walk;
            tempDir = transform.position - target.position;
            MoveInDirection(tempDir);
            if (Vector2.Distance(transform.position, target.transform.position) <= rangedRadius && Time.time > lastAttack + attCooldown)
            {
                RangedAttack();
                lastAttack = Time.time;
            }
        }        
        else if(currentState != EnemyState.dead && Vector2.Distance(transform.position, target.position) <= attackRadius)
        {            
            temp = Vector2.MoveTowards(transform.position, -target.position , moveSpeed * 2f);
            thisBody.MovePosition(temp);
            castV3 = transform.position;
            tempDir = target.position;
            temp = castV3 - tempDir;
            MoveInDirection(-temp);
        }
        else if(currentState != EnemyState.dead && Vector2.Distance(transform.position, target.position) == attackRadius)
        {
            attackRadius = 3.5f;
        }
        else if (currentState != EnemyState.dead)
        {//makes the enemy return to it's home position            
            temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed);
            thisBody.MovePosition(temp);//moves the enemy
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
            if (Vector2.Distance(transform.position, target.position) <= rangedRadius)
            {
                if ((Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .02f) || (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .02f))
                {
                    //This will fire a projectile along the x axis
                    // Author Joel Monteon
                    if (transform.position.x < target.position.x && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .2f)
                        && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) > -.2f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileForce, 0f));
                    }
                    if (transform.position.x > target.position.x && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) < .2f)
                        && (Mathf.Abs(transform.position.y) - Mathf.Abs(target.position.y) > -.2f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileForce, 0f));
                    }
                    //This will fire a projectile along the y axis
                    if (transform.position.y < target.position.y && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .2f)
                       && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) > -.2f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, projectileForce));
                    }
                    if (transform.position.y > target.position.y && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) < .2f)
                        && (Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x) > -.2f))
                    {
                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -projectileForce));
                    }
                }
            }
        }
    }
}
