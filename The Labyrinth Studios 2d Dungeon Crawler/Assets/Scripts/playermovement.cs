using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    Vector2 movement;
    public Animator animator;
    public Transform attackArea;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            attackArea.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            attackArea.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            attackArea.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            attackArea.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
