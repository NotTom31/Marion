using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle, walk, attack, stagger
}
public class Enemy1 : MonoBehaviour
{
    public EnemyState currentState;
    public GameObject Player;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageTaken)
    {
        GameObject.Find("Hit Sfx").GetComponent<AudioSource>().Play();
        health -= damageTaken;        // Play hurt animation if we have the hurt sprites
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    public void DamagePlayer(int damageGiven)
    {
        if (Player.gameObject.name == "player")
        {          
            //Player.GetComponent<playermovement>().TakeDamage(damageGiven);           
            Debug.Log("the player has been hit");            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other != null)
            {
                Player.GetComponent<playermovement>().TakeDamage(baseAttack);
            }

        }
    }
}
