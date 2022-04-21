using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTakeInput : Collectable
{
    public UnityEvent DoorOn;
    public UnityEvent DoorOff;
    public UnityEvent DisplayText;
    public bool Locked = false;
    public UnityEvent ActivatePortal;
    protected override void OnCollect()
    {
        if (Input.GetKeyDown("e") && Locked == false)
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
        else if (Input.GetKeyDown("e") && Locked == true)
        {
            DoorLocked();
        }

    }

    public void DoorLocked()
    {
        DisplayText.Invoke();
    }

    public void UnlockDoor()
    {
        Locked = false;
    }

    public void LockDoor()
    {
        Locked = true;
    }
}




