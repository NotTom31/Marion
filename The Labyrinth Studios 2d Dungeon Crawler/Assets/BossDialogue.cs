using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDialogue : Collectable
{
    public UnityEvent DisplayText;
    public UnityEvent CloseText;


    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            DisplayText.Invoke();
            Debug.Log("Im here.");
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            Debug.Log("Walking away.");
        }
    }
}




