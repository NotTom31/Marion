using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script is for a base enemy class, this will be the parent class to each enemy type's own script file
public class BaseEnemy : MonoBehaviour
{
    // Setting variables for enemy combat
    public float maxHealth;
    protected float health;
    public Image healthImage;
    public float speed;
    public float runSpeed;
    public float chaseRange;
    public float attackRange;

    // Setting enemy states (enemies will always be in "move" state by default
    public enum enemystates { move, chase, attack }
    public enemystates currentState = enemystates.move;
    public float rayLength;

    protected Rigidbody2D enemyRigidbody;

    protected PlayerCombat player;

    // Attack CD
    public float timeBetweenAttacks;
    protected float attackCooldown;

    protected Animator anim;

    private void OnEnable()
    {
        health = maxHealth;
        attackCooldown = timeBetweenAttacks;
    }
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerCombat>();
        anim = GetComponent<Animator>();
    }

    public virtual void Move() { }
    public virtual void Chase() { }
    public virtual void Attack() { }
    public virtual void Damage(float amount) { }
    public virtual void Die() { }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case enemystates.move:
                Move();
                break;
            case enemystates.chase:
                Chase();
                break;
            case enemystates.attack:
                Attack();
                break;
        }
    }
}
