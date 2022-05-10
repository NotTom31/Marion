using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHitbox : Collectable
{
    public UnityEvent DisplayText;
    public UnityEvent CloseText;
    public UnityEvent InteractIconOn;
    public UnityEvent InteractIconOff;


    protected override void OnCollect()
    {
        if (Input.GetKeyDown("e"))
        {
            DisplayText.Invoke();
        }
        InteractIconOn.Invoke();
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            Debug.Log("Walking away.");
            InteractIconOff.Invoke();
        }
    }
}




