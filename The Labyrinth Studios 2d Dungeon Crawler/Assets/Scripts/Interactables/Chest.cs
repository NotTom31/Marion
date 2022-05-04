using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public UnityEvent ChestOpened;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            ChestOpened.Invoke();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + pesosAmount + " pesos!");
        }
    }
    
}
