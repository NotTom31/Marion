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
    protected float lastAttack = -9999f;
    //*******************************************************************************************************************************************************************
    //********************************************************INITIALIZATION*********************************************************************************************
    //*******************************************************************************************************************************************************************
}
