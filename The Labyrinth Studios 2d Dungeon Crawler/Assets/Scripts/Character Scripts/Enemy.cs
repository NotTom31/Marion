using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**********************************************************************************************************************************************************************
//********************************************************ENEMY STATE MACHINE*******************************************************************************************
//**********************************************************************************************************************************************************************
public enum EnemyState//Different states the enemy can have
{
    idle, walk, attack, stagger, dead, teleport
}
public class Enemy : Character
{
    //*******************************************************************************************************************************************************************
    //********************************************************ENEMY CLASS ATTRIBUTES*************************************************************************************
    //*******************************************************************************************************************************************************************
    public EnemyState currentState;//will determine the current state of the enemy
    protected Transform target;//will be our player
    protected float chaseRadius;//the radius in which the enemy will activate and chase the player
    protected float attackRadius;//how far away we want the enemy to stop from the player
    public float reviveRadius;//this will initiate the Revive routine and is used in the Player Script
    public Vector2 homePosition;//the enemy's home position so it can return after chasing
    //public float rangedRadius;//This will be used for the ranged enemy attack radius
    //public GameObject projectile;//this will be created by the ranged enemy as a projectile
    public GameObject heart;//this will be used when the enemy dies and drops a heart, or 1 life point
                            
}
