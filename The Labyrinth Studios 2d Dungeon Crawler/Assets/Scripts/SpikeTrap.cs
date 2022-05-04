using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpikeTrap : MonoBehaviour , IPushable
{
    public GameObject tripWire;
    private void Start()
    {
        thrust = 4f;
        pushTime = .1f;
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(this.tripWire.GetComponent<TripWire>().trapenabled == TrapEnabled.enabled && obj.CompareTag("Player"))
        {
            obj.GetComponent<Character>().Damage(1, obj);
            Push(obj);
        }
    }
    public float thrust { get; set; }//how hard an object is pushed back
    public float pushTime { get; set; }//how long the push back last
    public void Push(Collider2D obj)//Author Johnathan Bates
    {
        Rigidbody2D character = obj.GetComponent<Rigidbody2D>();//reference to the Rigidbody component
        if (character != null)//makes sure the object hasn't already been destroyed
        {
            Vector2 difference = character.transform.position - transform.position;//not sure lol
            difference = difference.normalized * thrust;//this is where the thrust will change how far something is pushed back
            character.AddForce(difference, ForceMode2D.Impulse);//the actual push occurs here
            StartCoroutine(PushCo(character));
        }
    }
    public IEnumerator PushCo(Rigidbody2D character)//Author Johnathan Bates
    {
        if (character != null)
        {
            yield return new WaitForSeconds(pushTime);
            character.velocity = Vector2.zero;
        }
    }
}