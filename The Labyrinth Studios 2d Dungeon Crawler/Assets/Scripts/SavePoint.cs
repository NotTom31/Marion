using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour 
{
    public GameObject saveGame;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("Player"))
        {
            GameObject temp = GameObject.Find("player");
            if (temp.GetComponent<Player>().currentHealth < temp.GetComponent<Player>().maxHealth)
            {
                temp.GetComponent<Player>().currentHealth = 3;
            }
            saveGame.SetActive(true);
            this.GetComponent<Animator>().SetBool("on", true);
        }
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            saveGame.SetActive(false);
            this.GetComponent<Animator>().SetBool("on", false);
            
        }
    }    
}
