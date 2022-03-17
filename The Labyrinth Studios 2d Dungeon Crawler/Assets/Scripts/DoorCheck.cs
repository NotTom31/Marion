using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : Door
{

    protected override void OnCollect()
    {
        if (Input.GetKeyDown("f"))
        {
            if (!open)
            {
                Open();
            }
        }
    }

    protected override void Open()
    {
        open = false;
    }

    /*else
    {
        GetComponent<SpriteRenderer>().sprite = closedDoor;
        gameObject.layer = 8;
    }*/
}