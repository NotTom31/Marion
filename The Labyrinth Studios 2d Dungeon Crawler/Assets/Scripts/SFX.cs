using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource footSteps;
    void Update()
    {
        if ( Input.GetAxis("Horizontal") == 0)
        {
            footSteps.Play();
        }
        /*
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            footSteps.Play();
        }
        */
    }
}
