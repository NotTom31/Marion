using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collectable
{
    public Sprite openDoor;
    public Sprite closedDoor;
    protected bool open;
    

    protected virtual void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openDoor;
        gameObject.layer = 0;
    }


}


