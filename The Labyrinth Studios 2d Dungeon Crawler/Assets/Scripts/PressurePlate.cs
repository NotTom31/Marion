using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : Collectable 
{
    public UnityEvent PlatePushed;
    public UnityEvent PlateReleased;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        this.GetComponent<Animator>().SetBool("on", true);
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
     this.GetComponent<Animator>().SetBool("on", false);
        
    }
}
