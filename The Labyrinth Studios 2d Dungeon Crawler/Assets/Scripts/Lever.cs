using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : Collectable
{
    public Sprite onLever; 
    public Sprite offLever;
    public UnityEvent LeverOn;
    public UnityEvent LeverOff;
    public UnityEvent ActivatePortal;
    protected override void OnCollect()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!collected)
            {
                collected = true;
                GetComponent<SpriteRenderer>().sprite = onLever; 
                LeverOn.Invoke(); //run unity events
                ActivatePortal.Invoke(); //activate a warp if we want
                transform.localScale = Vector3.one; //flip the sprite
            }
            else
            {
                collected = false;
                GetComponent<SpriteRenderer>().sprite = offLever; //if we ever get an off lever sprite this will change the sprite instead of mirroring it
                LeverOff.Invoke(); //run unity events
                ActivatePortal.Invoke(); 
                transform.localScale = new Vector3(-1, 1, 1); //flip the sprite
            }
        }
    }

}




