using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : Collectable
{
    public UnityEvent BoxLift;
    public UnityEvent BoxLayer;
    public UnityEvent BoxLayerTwo;
    public float LastFacingHorizontal = 0;
    public float LastFacingVertical = 0;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            LastFacingHorizontal = Input.GetAxisRaw("Horizontal");
            LastFacingVertical = Input.GetAxisRaw("Vertical");
        }

    }

    protected override void OnCollect()
    {
        if (collected)
        {
            BoxFacing();
        }
        if (Input.GetKeyDown("e")) //interact button
        {
            if (!collected)
            {
                collected = true;
                BoxLift.Invoke(); //attaches the box to the player
                BoxLayer.Invoke(); //initial setting the boxes local position and layer to keep it consistant
                //transform.localPosition = new Vector3(0, 0, 0);
                BoxFacing();
            }
            else
            {
                BoxDrop();
            }
        }
    }

    protected void BoxDrop()
    {
        collected = false;

        if (Input.GetAxisRaw("Horizontal") > 0 || LastFacingHorizontal > 0)
        {
            transform.localPosition = new Vector3(0.1f, -0.2f, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 || LastFacingHorizontal < 0)
        {
            transform.localPosition = new Vector3(-0.1f, -0.2f, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Vertical") > 0 || LastFacingVertical > 0)
        {
            transform.localPosition = new Vector3(0, 0.1f, 0);
            BoxLayer.Invoke(); //sets the box behind the player
        }
        if (Input.GetAxisRaw("Vertical") < 0 || LastFacingVertical < 0)
        {
            transform.localPosition = new Vector3(0, -0.44f, 0);
            BoxLayerTwo.Invoke(); //sets the box in front of the player
        }
        BoxLayer.Invoke();
        transform.parent = null; //detatches the box
    }


    protected void BoxFacing() //checks if you're holding the box, and moves the box relative to the players local position based on button inputs
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            LastFacingHorizontal = Input.GetAxisRaw("Horizontal");
            LastFacingVertical = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || LastFacingHorizontal > 0)
        {
            transform.localPosition = new Vector3(0.1f, 0, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 || LastFacingHorizontal < 0)
        {
            transform.localPosition = new Vector3(-0.1f, 0, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Vertical") > 0 || LastFacingVertical > 0)
        {
            transform.localPosition = new Vector3(0, 0.1f, 0);
            BoxLayer.Invoke(); //sets the box behind the player
        }
        if (Input.GetAxisRaw("Vertical") < 0 || LastFacingVertical < 0)
        {
            transform.localPosition = new Vector3(0, -0.1f, 0);
            BoxLayerTwo.Invoke(); //sets the box in front of the player
        }
    }

}




