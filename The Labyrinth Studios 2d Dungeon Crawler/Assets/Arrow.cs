using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D thisBody;

    private void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.CompareTag("Fighter") || other.CompareTag("BossSummon") || other.CompareTag("Boss"))
        {
            other.GetComponent<Character>().Push(other);
            other.GetComponent<Character>().Damage(3, other);
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
