using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeR1 : BaseEnemy
{
    public GameObject baseball;

    public override void Move()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, rayLength, wallLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.right * direction - Vector2.up, rayLength, wallLayer);


        if (hit.collider != null) direction *= -1;

        if (hitDown.collider == null) direction *= -1;
        Debug.DrawRay(transform.position, Vector2.right * direction * rayLength);

        if (distance <= chaseRange)
            currentState = enemystates.chase;
        enemyRigidbody.AddForce(Vector2.right * direction * speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    public override void Damage(float amt)
    {
        health -= amt;
        if (health <= 0) Die();
    }

    // Update is called once per frame
    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public override void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (transform.position.x > player.transform.position.x)
            direction = -1;
        else
            direction = 1;

        if (distance >= chaseRange)
            currentState = enemystates.move;
        if (distance <= attackRange)
            currentState = enemystates.attack;

        enemyRigidbody.AddForce(Vector2.right * direction * runSpeed * Time.deltaTime);

    }

    public override void Attack()
    {
        if (attackCools <= 0)
        {
            anim.SetBool("attack", true);

            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Instantiate(baseball, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

            Invoke("ResetAttack", 1f);
            attackCools = timeBetweenAttacks;

        }
        else
            currentState = enemystates.chase;

    }

    void ResetAttack()
    {
        anim.SetBool("attack", false);
    }
}
