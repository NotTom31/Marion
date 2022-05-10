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
        if (other.CompareTag("Wall") || other.CompareTag("Fighter"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Fighter"))
        {
            other.GetComponent<Character>().Push(other);
            other.GetComponent<Character>().Damage(1, other);
            Destroy(this.gameObject);
        }
    }
}
