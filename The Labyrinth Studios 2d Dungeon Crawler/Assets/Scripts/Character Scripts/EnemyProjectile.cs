using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour 
{
    private float autoDestroyTime = 3f;
    private float moveSpeed = 2;
    private int damage = 1;
    public Rigidbody2D thisBody;

    private void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}   
