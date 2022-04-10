using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : Collectable
{
    public UnityEvent BoxLift;
    public UnityEvent BoxLayer;
    public UnityEvent BoxLayerTwo;
    protected override void OnCollect()
    {

        if (Input.GetKeyDown("e")) //interact button
        {
            if (!collected)
            {
                collected = true; 
                BoxLift.Invoke(); //attaches the box to the player

                BoxLayer.Invoke(); //initial setting the boxes local position and layer to keep it consistant
                transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                collected = false;
                transform.parent = null; //detatches the box
            }
        }


        if (collected) //checks if you're holding the box, and moves the box relative to the players local position based on button inputs
        {
            if (Input.GetKeyDown("w")) //in the future this probably shouldn't be tied to the keyboard input
            {
                transform.localPosition = new Vector3(0, 0.1f, 0);
                BoxLayer.Invoke(); //sets the box behind the player
            }
            else if (Input.GetKeyDown("a"))
            {
                transform.localPosition = new Vector3(-0.1f, 0, 0);
                BoxLayer.Invoke();
            }
            else if (Input.GetKeyDown("s"))
            {
                transform.localPosition = new Vector3(0, -0.1f, 0);
                BoxLayerTwo.Invoke(); //sets the box in front of the player
            }
            else if (Input.GetKeyDown("d"))
            {
                transform.localPosition = new Vector3(0.1f, 0, 0);
                BoxLayer.Invoke();
            }

        }
    }

}




