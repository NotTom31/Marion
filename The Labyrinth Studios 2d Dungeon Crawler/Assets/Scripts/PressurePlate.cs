using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : Collectable
{
    public UnityEvent PlatePushed;
    public UnityEvent PlateReleased;

/*    void FixedUpdate()
    {
        if (!collected)
        {
            PlateReleased.Invoke();
        }
    }*/

   /* private void OnTriggerStay(Collider PressurePlate)
    {
        collected = true;
        PlatePushed.Invoke();
        Debug.Log("Plate Pushed");
    }

    private void OnTriggerExit(Collider PressurePlate)
    {
        collected = false;
        PlateReleased.Invoke();
        Debug.Log("Plate Released");
    }*/

    protected override void OnCollect()
    {
        if (!collected && transform.parent == null)
        {
            collected = true;
            PlatePushed.Invoke(); 
            Debug.Log("Plate Pushed");
        }
        else 
        {
            collected = false;
            PlateReleased.Invoke();
            Debug.Log("Plate Released");
        }
    }
}
