using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    public float speed;
    Rigidbody2D baseballRigidbody;
    public float damage;

    private void Awake()
    {
        baseballRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        baseballRigidbody.AddForce(transform.up * speed);
    }
    // Start is called before the first frame update

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damage(damage);
            Invoke("Disable", 0.01f);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Invoke("Disable", 0.01f);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
