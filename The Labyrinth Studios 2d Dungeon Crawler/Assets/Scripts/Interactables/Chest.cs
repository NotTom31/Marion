using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public bool HasKey = false;
    public UnityEvent ChestOpened;
    public UnityEvent KeyAnimation;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            ChestOpened.Invoke();
            GameObject.Find("Door Open SFX").GetComponent<AudioSource>().Play();//I thought this might sound good for the chest but we can change it 
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + pesosAmount + " pesos!");
            if (HasKey == true)
            {
                KeyManager.instance.AddKey();
                KeyAnimation.Invoke();
            }
        }
    }
    
}
