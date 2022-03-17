using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Collectable
{
    public Sprite onLever; 
    public Sprite offLever;
    public int LeverOn = 0;

    protected override void OnCollect()
    {
        if (Input.GetKeyDown("f"))
        {
            if (!collected)
            {
                collected = true;
                GetComponent<SpriteRenderer>().sprite = onLever;
                LeverOn = 1;
                transform.localScale = Vector3.one;
            }
            else
            {
                collected = false;
                GetComponent<SpriteRenderer>().sprite = offLever;
                LeverOn = 0;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

}
