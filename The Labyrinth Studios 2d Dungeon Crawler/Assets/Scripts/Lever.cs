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
                LeverOn.Invoke();
                ActivatePortal.Invoke();
                transform.localScale = Vector3.one;
            }
            else
            {
                collected = false;
                GetComponent<SpriteRenderer>().sprite = offLever;
                LeverOff.Invoke();
                ActivatePortal.Invoke();
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

}




