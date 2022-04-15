using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTakeInput : Collectable
{
    public UnityEvent DoorOn;
    public UnityEvent DoorOff;
    public UnityEvent ActivatePortal;
    protected override void OnCollect()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!collected) 
            {
                collected = true;
                DoorOn.Invoke(); //runs the unity events to disable the door
                GameObject.Find("Door Open SFX").GetComponent<AudioSource>().Play();//plays open door sfx
                ActivatePortal.Invoke();
            }
            else
            {
                collected = false;
                DoorOff.Invoke(); //enables the door
                ActivatePortal.Invoke();
            }
        }
    }

}




