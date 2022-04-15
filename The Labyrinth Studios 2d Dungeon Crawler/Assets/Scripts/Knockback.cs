/*This is the general knockback file that will be used by multiple enemies as well as the character's attack
 to enable knockback from an enemy, add an empty object to an enemy, add a boxcollider2d, enable is trigger, 
then add this script to the empty object, then input Player in tag and set your thrust and knocktime*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;//how hard an object is knocked back
    public float knockTime;//how long the knock back last
    public string Tag;//this is be the object that gets knocked back, make sure to use a valid tag


    private void OnTriggerEnter2D(Collider2D other)//used by a box collider
    {
        if(other.gameObject.CompareTag(Tag))//checks for the tag of the object to be knocked
        {
            Rigidbody2D character = other.GetComponent<Rigidbody2D>();//gets the Rigidbody component
            if (character != null)//makes sure the object hasn't already been destroyed
            {                
                Vector2 difference = character.transform.position - transform.position;//not sure lol
                difference = difference.normalized * thrust;//this is where the thrust will change how far something is knocked back
                character.AddForce(difference, ForceMode2D.Impulse);//the actual knockback occurs here
                StartCoroutine(KnockCo(character));
            }
        }
    }   

    private IEnumerator KnockCo(Rigidbody2D character)
    {
        if (character != null)
        {
            yield return new WaitForSeconds(knockTime);
            character.velocity = Vector2.zero;
        }
    }

}
