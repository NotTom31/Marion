using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
//******************************************************************************************************************************************************
//********************************************************PLAYER STATE MACHINE**************************************************************************
//******************************************************************************************************************************************************
public enum PlayerState
{
    walk, attack, interact
}
public class Player : Character 
{
    //******************************************************************************************************************************************************
    //********************************************************PLAYER CLASS ATTRIBUTES***********************************************************************
    //******************************************************************************************************************************************************
    public PlayerState currentState;//variable for player state machine
    public Image[] hearts;//array to hold hearts gui
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int numOfHearts;
    public UnityEvent PlayerDied;
    //******************************************************************************************************************************************************
    //********************************************************INITIALIZATION********************************************************************************
    //******************************************************************************************************************************************************
    void Awake()
    {
        thisBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        charType = CharacterType.player;//state machine, what is the type of player
        PlayerState currentState = PlayerState.walk;//initialize playerstate to walk
        currentHealth = 5;
        thrust = 4.5f;
        pushTime = .13f;
    }
    //******************************************************************************************************************************************************
    //*****************************************************************ATTACKING INPUT**********************************************************************
    //******************************************************************************************************************************************************
    void Update()
    {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            getMoveInput();
        }
        heartGUI();
       
    }
    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);//allow animation
        currentState = PlayerState.attack;//player state machine
        yield return null;//wait for a frame
        anim.SetBool("attacking", false);//allow animation to continue
        yield return new WaitForSeconds(.33f);//wait for a third of a second
        currentState = PlayerState.walk;//resetting player state machine
    }
    //******************************************************************************************************************************************************
    //*****************************************************************MOVEMENT*****************************************************************************
    //******************************************************************************************************************************************************
    private void getMoveInput()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement != Vector2.zero)
        {
            Move(thisBody, movement, moveSpeed);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }
    
      
    //******************************************************************************************************************************************************
    //*****************************************************************HEALTH GUI***************************************************************************
    //******************************************************************************************************************************************************
    private void heartGUI()
    {
        if (currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth) { hearts[i].sprite = fullHeart; }
            else {  hearts[i].sprite = emptyHeart; }

            if (i < numOfHearts) { hearts[i].enabled = true; }
            else{ hearts[i].enabled = false; }
        }
    }
    //******************************************************************************************************************************************************
    //*****************************************************************VISUAL EFFECTS***********************************************************************
    //******************************************************************************************************************************************************
    public IEnumerator playerBlink(GameObject other)//Coroutine to make the player blink whenever the player takes damage
                                                     //this code can probably be improved
    {
        for(int i = 6; i > 0; i--)
        {
            SpriteRenderer temp = other.GetComponent<SpriteRenderer>();
            temp.enabled = false;
            yield return new WaitForSeconds(.05f);
            temp.enabled = true;
            yield return new WaitForSeconds(.05f);
        }
    }   
}
