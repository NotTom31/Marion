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

        if (Input.GetKeyDown("e"))
        {
            if (!collected)
            {
                collected = true;
                BoxLift.Invoke();
                BoxLayer.Invoke();
                transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                collected = false;
                transform.parent = null;
            }
        }


        if (collected)
        {
            if (Input.GetKeyDown("w"))
            {
                transform.localPosition = new Vector3(0, 0.1f, 0);
                BoxLayer.Invoke();
            }
            else if (Input.GetKeyDown("a"))
            {
                transform.localPosition = new Vector3(-0.1f, 0, 0);
                BoxLayer.Invoke();
            }
            else if (Input.GetKeyDown("s"))
            {
                transform.localPosition = new Vector3(0, -0.1f, 0);
                BoxLayerTwo.Invoke();
            }
            else if (Input.GetKeyDown("d"))
            {
                transform.localPosition = new Vector3(0.1f, 0, 0);
                BoxLayer.Invoke();
            }

        }
    }

}




