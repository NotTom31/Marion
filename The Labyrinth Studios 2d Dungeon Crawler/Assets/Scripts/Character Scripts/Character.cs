using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

using UnityEngine.Events;



public enum CharacterType//State machine for the type of character
{
    player, meleeEnemy, rangedEnemy, npc
}
public class Character : MonoBehaviour, IMoveable, IDamageable , IKillable, IPushable 
{
    // Variables
    // Variables
    bool hurt;
    public float maxHealth;
    [SerializeField]
    float health;
    public float timeBetweenHurt;
    float iframe;
    public GameObject gameoverUI;
    bool gameover;

    //******************************************************************************************************************************************************
    //************************************************************DECLARING IMOVABLE************************************************************************
    //******************************************************************************************************************************************************
    public void Move(Rigidbody2D thisBody, Vector2 movement, float moveSpeed)//Author Johnathan Bates
    {
        if (thisBody.CompareTag("Player"))//checks to see if this is the player moving
        {
            thisBody.MovePosition(thisBody.position + movement * moveSpeed * Time.fixedDeltaTime);//actually moves the player
            anim.SetFloat("moveX", movement.x);//allows movement animation
            anim.SetFloat("moveY", movement.y);//allows movement animation
            anim.SetBool("moving", true);//moving set true in animator
        }
        if(thisBody.CompareTag("Fighter"))//checks to see if this is an enemy moving
        {
            thisBody.MovePosition(movement);//moves the enemy
        }
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDamageable*********************************************************************
    //******************************************************************************************************************************************************
    public void Damage(int damage, Collider2D obj)//Author Johnathan Bates
    {
        GameObject temp = obj.gameObject;//reference to the gameobject attatched to obj
        GameObject.Find("Hit Sfx").GetComponent<AudioSource>().Play();//sfx for getting hit 
        if (temp.GetComponent<Character>().currentHealth > 0)//check if they still have health
        {            
            if (obj.CompareTag("Player") && obj.GetComponent<Player>().currentState != PlayerState.stagger && damage > 0)//Player damaged, will  run the blink routine and set to stagger
            {
                StartCoroutine(temp.GetComponent<Player>().playerInvulnerable(temp));
                StartCoroutine(temp.GetComponent<Player>().playerBlink(temp));//start coroutine
            }
            if (obj.CompareTag("Fighter"))//Enemy damaged, will set state to stagger
            {
                temp.GetComponent<Enemy>().currentState = EnemyState.stagger;
            }
            temp.GetComponent<Character>().currentHealth -= damage;//compute damage 
            if (temp.GetComponent<Character>().currentHealth <= 0)//check if they should be dead
            { Kill(obj.gameObject); }//KILL THEM!!!!
        }            
    }

    // idoa;sfkj
    public void Damage(float amt)
    {
        if (iframe < 0)
        {
            health -= amt;
            hurt = true;
            Invoke("ResetHurt", 0.2f);
            if (health <= 0)
            {
                GameOver();
            }
            iframe = timeBetweenHurt;
        }
    }

    // asdjfhadsof
    private void GameOver()
    {
        gameover = true;
        gameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IKILLABLE***********************************************************************
    //******************************************************************************************************************************************************
    public void Kill(GameObject obj)//Author Johnathan Bates
    {
        GameObject temp = obj;//reference to the gameobject
        if(obj.CompareTag("Player"))//check if it is the player that has died
        {
           temp.GetComponent<Player>().PlayerDied.Invoke();//kills player
        }
        else if (obj.CompareTag("Fighter")) 
        
        { 
            temp.gameObject.SetActive(false); temp.GetComponent<Enemy>().currentState = EnemyState.dead; 
        }   //kills everything else   
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IPUSHABLE***********************************************************************
    //******************************************************************************************************************************************************
    public float thrust { get;  set; }//how hard an object is pushed back
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
    //******************************************************************************************************************************************************
    //********************************************************CHARACTER CLASS ATTRIBUTES********************************************************************
    //******************************************************************************************************************************************************
    public Rigidbody2D thisBody;//Use this in a child class or inspector to initialize RigidBody
    protected Animator anim;//Use this in a child class or inspector to initialize Animator
    protected Vector2 movement;//Use this in a child class to control movement 
    public float moveSpeed;//Use this in a child class or inspector tocontrol movement speed
    public CharacterType charType;//Use this in the Inspector to initialize what type of character it is
    public int attackDamage;//Use this in a child class or inspector to initialize RigidBody
    public int currentHealth;//Use this in a child class or inspector to initialize health
    //******************************************************************************************************************************************************
    //*****************************************************************MELEE ATTACKING TRIGGERED************************************************************
    //******************************************************************************************************************************************************
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if ((this.CompareTag("Player") && obj.CompareTag("Fighter")) || (obj.CompareTag("Player") && this.CompareTag("Fighter")))//check to make sure either player hits enemy or enemy hits player
        {
            if (obj.gameObject != null)
            {                
                Damage(attackDamage, obj);           
                Push(obj);
            }
        }
        // Author Joel Monteon
        if (this.CompareTag("Player") && obj.CompareTag("Heart"))
        {
            if (this.currentHealth < this.maxHealth)
            {
                Damage(-1, this.GetComponent<Collider2D>());
                Destroy(obj.gameObject);
            }
            
        }

        


    }    

}
