using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrapEnabled
{
    enabled, disabled
}
public class TripWire : MonoBehaviour
{
    public UnityEvent WireTripped;
    public TrapEnabled trapenabled;
    private void Start()
    {
        trapenabled = TrapEnabled.disabled;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            WireTripped.Invoke();
            trapenabled = TrapEnabled.enabled;
        }
    }
}