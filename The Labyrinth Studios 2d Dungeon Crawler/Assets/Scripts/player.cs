using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : Mover
{
    // Animator object
    public Animator animator;

    /*
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    } */
    
   private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (moveDelta != Vector3.zero)
        {
            animator.SetFloat("Horizontal", moveDelta.x);
            animator.SetFloat("Vertical", moveDelta.y);
            animator.SetFloat("Speed", moveDelta.sqrMagnitude);

            if (Input.GetAxisRaw("Horizontal") ==  1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            }

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