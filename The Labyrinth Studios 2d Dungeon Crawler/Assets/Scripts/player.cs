using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Mover
{
    public Animator animator;

   private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (moveDelta != Vector3.zero)
        {
            animator.SetFloat("Horizontal", moveDelta.x);
            animator.SetFloat("Vertical", moveDelta.y);
            animator.SetFloat("Speed", moveDelta.sqrMagnitude);

        }

        UpdateMotor(new Vector3(x, y, 0));
    }
}

// suggestion for movement - jason
/*
// initializing variables 
public float Speed;
Vector2 input;
Rigidbody2D playerRigidbody;

void Start()
{
    playerRigidbody = GetComponent<Rigidbody2D>();
}

void Update()
{
    input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    playerRigidbody.AddForce(input * speed * Time.deltaTime);
}
*/
// change speed value in Unity Editor
// need to add Rigidbody 2D on player character