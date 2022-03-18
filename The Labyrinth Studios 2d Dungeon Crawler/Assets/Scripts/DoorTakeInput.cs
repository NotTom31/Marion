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
        if (Input.GetKeyDown("f"))
        {
            if (!collected)
            {
                collected = true;
                DoorOn.Invoke();
                ActivatePortal.Invoke();
            }
            else
            {
                collected = false;
                DoorOff.Invoke();
                ActivatePortal.Invoke();
            }
        }
    }

}




