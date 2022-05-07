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
public enum PlayerState//Different states the player can have
{
    walk, attack, interact, stagger, dead, holdingBox, holdingBow
}
public class Player : Character , IDataPersistence, IMoveable
{
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
        if(this.currentHealth == 0)
        {
            this.currentHealth = 3;
        }
        this.transform.position = data.playerPosition;
        Debug.Log(data.playerPosition);
    }
    public void SaveData(GameData data)
    {
        data.currentHealth = this.currentHealth;
        data.playerPosition = this.transform.position;
        Debug.Log(data.playerPosition);
    }   

    //******************************************************************************************************************************************************
    //********************************************************PLAYER CLASS ATTRIBUTES***********************************************************************
    //******************************************************************************************************************************************************
    public PlayerState currentState;//variable for player state machine
    public Image[] hearts;//array to hold hearts gui
    public Sprite fullHeart;//image of a heart for the health bar
    public Sprite emptyHeart;//image of an empty heart for the health bar
    public int numOfHearts;//how many hearts are in the health bar
    public UnityEvent PlayerDied;//when the player dies, the event will kick off, which will be the game over screen
    protected GameObject[] inRange;//array that will hold all enemies, will be used to revive them
    //******************************************************************************************************************************************************
    //********************************************************INITIALIZATION********************************************************************************
    //******************************************************************************************************************************************************
    void Start()
    {          
        charType = CharacterType.player;//state machine, what is the type of character
        PlayerState currentState = PlayerState.walk;//initialize playerstate to walk        
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component        
        inRange = GameObject.FindGameObjectsWithTag("Fighter");
        moveSpeed = 1.25f;
    }
    //******************************************************************************************************************************************************
    //*****************************************************************UPDATE*******************************************************************************
    //******************************************************************************************************************************************************
    void Update()
    {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)//checks for user input to attack
        {
            if (this.currentState != PlayerState.holdingBox)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (currentState == PlayerState.walk)//checks for player movement
        {
            Move();
        }
        heartGUI();//manages heart display
        ReviveInRange();//revives enemies once out of a certain range
        ReviveInRange();//revives enemies once out of a certain range        
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
    //************************************************************DECLARING IMOVABLE************************************************************************
    //******************************************************************************************************************************************************
    public void Move()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement != Vector2.zero)
        {
            thisBody.MovePosition(thisBody.position + movement * moveSpeed * Time.fixedDeltaTime);//actually moves the player
            anim.SetFloat("moveX", movement.x);//allows movement animation
            anim.SetFloat("moveY", movement.y);//allows movement animation
            anim.SetBool("moving", true);//moving set true in animator
            currentState = PlayerState.walk;
        }
        else
        {
            anim.SetBool("moving", false);
            currentState = PlayerState.walk;
        }
    }     
    //******************************************************************************************************************************************************
    //*****************************************************************HEALTH GUI***************************************************************************
    //******************************************************************************************************************************************************
    private void heartGUI()
    {
        if (currentHealth > numOfHearts)
        { currentHealth = numOfHearts; }
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
                                                    //Author Johnathan Bates
    {
        for (int i = 10; i > 0; i--)
        {
            SpriteRenderer temp = other.GetComponent<SpriteRenderer>();
            temp.enabled = false;
            yield return new WaitForSeconds(.01f);
            temp.enabled = true;
            yield return new WaitForSeconds(.015f);
        }
        this.currentState = PlayerState.walk;
    }

    public IEnumerator playerInvulnerable(GameObject obj)//Author Johnathan Bates
    {
        this.currentState = PlayerState.stagger;
        yield return new WaitForSeconds(.35f);
    }
    //******************************************************************************************************************************************************
    //*****************************************************************ENEMY MANAGEMENT*********************************************************************
    //******************************************************************************************************************************************************
    //ReviveInRange() revives an enemy once the player has reached a specific range. Range is determined by reviveRadius variable located in Enemy script
    //this method is needed here because once we disable an enemy, that enemy's script is also disabled thus it is unable to revive itself
    void ReviveInRange()//Author Johnathan Bates
    {        
        for (int i = 0; i < inRange.Length; i++)//cycle through the array
        {
            GameObject temp = inRange[i];
            
            //check if the enemy is no longer active and the player is far enough away from spawn point
            if (temp.GetComponent<Enemy>().currentState == EnemyState.dead && Vector2.Distance(transform.position, temp.GetComponent<Enemy>().homePosition) > temp.GetComponent<Enemy>().reviveRadius)
            { 
                temp.transform.position = temp.GetComponent<Enemy>().homePosition;//puts enemy back to spawn point
                temp.GetComponent<Enemy>().currentHealth = 3;// resets its health back to 3
                temp.GetComponent<Enemy>().currentState = EnemyState.idle;//resets the enemy state back to idle
                temp.gameObject.SetActive(true);//re activates enemy within the scene
            }
        }
    }
    //******************************************************************************************************************************************************
    //*****************************************************************MELEE ATTACKING TRIGGERED************************************************************
    //******************************************************************************************************************************************************
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if ( obj.CompareTag("Fighter"))//check to make sure either player hits enemy or enemy hits player
        {
            if (obj.gameObject != null)
            {
                Damage(attackDamage, obj);
                Push(obj);
            }
        }
        // Author Joel Monteon
        if (obj.CompareTag("Heart"))
        {
            if (this.currentHealth < this.maxHealth)
            {
                Damage(-1, this.GetComponent<Collider2D>());
                Destroy(obj.gameObject);
            }
        }
        if(obj.CompareTag("Projectile"))
        {
            Debug.Log(this.tag);
            Damage(1, this.GetComponent<Collider2D>());
            Push(this.GetComponent<Collider2D>());
        }
    }
}
