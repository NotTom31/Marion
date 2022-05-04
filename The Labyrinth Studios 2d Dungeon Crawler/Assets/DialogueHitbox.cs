using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHitbox : MonoBehaviour
{
    public UnityEvent DisplayText;
    public UnityEvent CloseText;


    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                DisplayText.Invoke();
            }
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




