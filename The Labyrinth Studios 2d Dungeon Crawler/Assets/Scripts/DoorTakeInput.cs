using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTakeInput : Collectable 
{
    public UnityEvent DoorOn;
    public UnityEvent DoorOff;
    public UnityEvent DisplayText;
    public UnityEvent CloseText;
    public bool Locked = false;
    public UnityEvent ActivatePortal;
    private Animator anim;

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
                anim.SetBool("DoorOpen", true);
            }
            else
            {
                collected = false;
                DoorOff.Invoke(); //enables the door
                ActivatePortal.Invoke();
                anim.SetBool("DoorOpen", false);
            }
        }
        else if (Input.GetKeyDown("e") && Locked == true)
        {
            DoorLocked();
        }

    }

    public void DoorLocked()
    {
        if (KeyManager.instance.KeyCount >= 1 && Locked == true)
        {
            UnlockDoor();
            KeyManager.instance.SubtractKey();
            anim = GameObject.Find("LockedDoor").GetComponent<Animator>();            
            anim.SetBool("DoorUnlock", true);
        }
        else
            DisplayText.Invoke();
    }

    public void UnlockDoor()
    {
        Locked = false;
        DoorOff.Invoke();
    }

    public void LockDoor()
    {
        Locked = true;
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            Debug.Log("Im here.");
        }
    }
}




