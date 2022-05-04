using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TripWire : MonoBehaviour
{
    public UnityEvent WireTripped;


    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            WireTripped.Invoke(); 
        }
    }
}