using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpikeTrap : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            /*if spike trap == active
             Ouch
            knockback
             */
        }
    }
}