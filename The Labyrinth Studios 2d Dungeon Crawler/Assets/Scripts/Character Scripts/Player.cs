using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
//******************************************************************************************************************************************************
//********************************************************PLAYER STATE MACHINE**************************************************************************
//******************************************************************************************************************************************************
public enum PlayerState//Different states the player can have
{
    walk, attack, interact, stagger, dead, holdingBox, holdingBow
}
public class Player : Character, IDataPersistence, IMoveable
{
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
        if (this.currentHealth == 0)
        {
            this.currentHealth = 3;
        }
        this.usedAPortal = data.usedAPortal;
        if (usedAPortal == false)
        { this.transform.position = data.playerPosition; }
        this.lastFacingHorizontal = data.lastHorizontalPlayer;
        this.lastFacingVertical = data.LastVerticalBox;
        this.hasDagger = data.hasDagger;
        this.hasCrossbow = data.hasCrossbow;
        this.holdingDagger = data.holdingDagger;
        this.holdingCrossbow = data.holdingCrossbow;
        Debug.Log("holding dagger is " + holdingDagger);
        Debug.Log("holding crossbow is " + holdingCrossbow);
        if (holdingDagger)
        {
            EquipDagger();
        }
        if(holdingCrossbow)
        {
            EquipBow();
        }
        anim.SetFloat("moveX", lastFacingHorizontal);//allows movement animation
        anim.SetFloat("moveY", lastFacingVertical);//allows movement animation
    }
    public void SaveData(GameData data)
    {
        
        data.currentHealth = this.currentHealth;
        if(usedAPortal == false)
        { data.playerPosition = this.transform.position; }
        if (usedAPortal == true)
        {
            data.playerPosition = new Vector3(0, 0, 0);
        }
        data.lastHorizontalPlayer = this.lastFacingHorizontal;
        data.lastVerticalPlayer = this.lastFacingVertical;
        data.holdingCrossbow = this.holdingCrossbow;
        data.holdingDagger = this.holdingDagger;
        data.hasDagger = this.hasDagger;
        Debug.Log("Saved holding dagger is " + data.holdingDagger);
        data.hasCrossbow = this.hasCrossbow;
        data.usedAPortal = this.usedAPortal;
    }
    private GameObject theCutscene;
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
    public GameObject arrow;
    private float projectileForce;
    public float lastFacingVertical;
    public float lastFacingHorizontal;
    public bool attacking = false;
    public bool usedAPortal = false;
    private bool hasDagger = false;
    private bool hasCrossbow = false;
    private bool holdingDagger = false;
    private bool holdingCrossbow = false;
    private GameObject arrowManager;
    //******************************************************************************************************************************************************
    //********************************************************INITIALIZATION********************************************************************************
    //******************************************************************************************************************************************************
    public void SetPortalBool()
    {
        usedAPortal = false;
    }
    void Awake()
    {
        charType = CharacterType.player;//state machine, what is the type of character   
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component        
        inRange = GameObject.FindGameObjectsWithTag("Fighter");

        moveSpeed = 1.25f;
        projectileForce = 80;
        if (SceneManager.GetActiveScene().name == "BossBattleScene")
        {
            theCutscene = GameObject.Find("TimeLineManager").GetComponent<GameObject>();
        }
        arrowManager = GameObject.Find("Arrow Count");
    }

    void Start()
    {
        if (hasDagger == false & hasCrossbow == false)
        {
            anim.SetBool("holdingDagger", true);
        }
    }
    //******************************************************************************************************************************************************
    //*****************************************************************UPDATE*******************************************************************************
    //******************************************************************************************************************************************************
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipDagger();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasCrossbow == true)
        {
            EquipBow();
        }

        if (anim.GetBool("holdingDagger"))
        {
            if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && hasDagger == true)//checks for user input to attack
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
        }
        if (anim.GetBool("holdingCrossbow"))
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
        }

        heartGUI();//manages heart display
        ReviveInRange();//revives enemies once out of a certain range
    }
    void EquipDagger()
    {
        anim.SetBool("holdingDagger", true);
        anim.SetBool("holdingCrossbow", false);
        holdingDagger = true;
        holdingCrossbow = false;
    }
    void EquipBow()
    {
        anim.SetBool("holdingDagger", false);
        anim.SetBool("holdingCrossbow", true);
        holdingCrossbow = true;
        holdingDagger = false;
    }
    private IEnumerator AttackCo()
    {
        if (beingPushed == false)
        {
            if (anim.GetBool("holdingDagger"))
            {
                if (this != null)
                {
                    attacking = true;
                    anim.SetBool("attacking", true);//allow animation
                    currentState = PlayerState.attack;//player state machine
                    yield return null;//wait for a frame
                }
                if (this != null)
                {
                    attacking = false;
                    anim.SetBool("attacking", false);//allow animation to continue
                    yield return new WaitForSeconds(.33f);//wait for a third of a second
                    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//stops the push
                }
                currentState = PlayerState.walk;//resetting player state machine
            }
            if (anim.GetBool("holdingCrossbow"))
            {

                Vector2 offset = new Vector2(0, 0);
                Vector3 castv;
                offset.x = anim.GetFloat("moveX");
                offset.y = anim.GetFloat("moveY");
                if (arrowManager.GetComponent<ArrowManager>().arrowCount > 0)
                {
                    arrowManager.GetComponent<ArrowManager>().SubtractArrow();
                    if (offset.x == 1f)
                    {
                        offset.x = .1f;
                        castv = offset;
                        GameObject newProjectile = Instantiate(arrow, transform.position + castv, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(projectileForce, 0f));
                        anim.SetBool("attacking", true);//allow animation
                        currentState = PlayerState.attack;//player state machine
                        yield return null;//wait for a frame
                        anim.SetBool("attacking", false);//allow animation to continue
                        yield return new WaitForSeconds(.33f);//wait for a third of a second
                        currentState = PlayerState.walk;//resetting player state machine

                    }
                    if (offset.x == -1f)
                    {
                        offset.x = -.1f;
                        castv = offset;
                        GameObject newProjectile = Instantiate(arrow, transform.position + castv, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-projectileForce, 0f));
                        anim.SetBool("attacking", true);//allow animation
                        currentState = PlayerState.attack;//player state machine
                        yield return null;//wait for a frame
                        anim.SetBool("attacking", false);//allow animation to continue
                        yield return new WaitForSeconds(.33f);//wait for a third of a second
                        currentState = PlayerState.walk;//resetting player state machine
                    }

                    if (offset.y == 1f)
                    {
                        offset.y = .1f;
                        castv = offset;
                        GameObject newProjectile = Instantiate(arrow, transform.position + castv, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, projectileForce));
                        anim.SetBool("attacking", true);//allow animation
                        currentState = PlayerState.attack;//player state machine
                        yield return null;//wait for a frame
                        anim.SetBool("attacking", false);//allow animation to continue
                        yield return new WaitForSeconds(.33f);//wait for a third of a second
                        currentState = PlayerState.walk;//resetting player state machine
                    }
                    if (offset.y == -1f)
                    {

                        offset.y = -.1f;
                        castv = offset;
                        GameObject newProjectile = Instantiate(arrow, transform.position + castv, transform.rotation * Quaternion.Euler(0f, 0f, -90f));
                        newProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -projectileForce));

                        anim.SetBool("attacking", true);//allow animation
                        currentState = PlayerState.attack;//player state machine
                        yield return null;//wait for a frame
                        anim.SetBool("attacking", false);//allow animation to continue
                        yield return new WaitForSeconds(.33f);//wait for a third of a second
                        currentState = PlayerState.walk;//resetting player state machine
                    }
                    castv = offset;

                }
            }
        }



    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IMOVABLE************************************************************************
    //******************************************************************************************************************************************************
    public void Move()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            lastFacingHorizontal = Input.GetAxisRaw("Horizontal");
            lastFacingVertical = Input.GetAxisRaw("Vertical");
        }
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
            else { hearts[i].sprite = emptyHeart; }
            if (i < numOfHearts) { hearts[i].enabled = true; }
            else { hearts[i].enabled = false; }
        }
    }

    public IEnumerator playerInvulnerable(GameObject obj)//Author Johnathan Bates
    {
        if (obj != null)
        {
            this.currentState = PlayerState.stagger;
            yield return new WaitForSeconds(.3f);
        }
        if (obj != null)
        { 
            this.currentState = PlayerState.walk;
        }
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
        if (obj.CompareTag("Fighter") || obj.CompareTag("BossSummon") || obj.CompareTag("Boss"))//check to make sure either player hits enemy or enemy hits player
        {
            if (obj != null)
            {
                Damage(attackDamage, obj);
            }
            if (obj != null)
            { 
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
        if (obj.CompareTag("Projectile"))
        {
            Push(this.GetComponent<Collider2D>());
            Damage(1, this.GetComponent<Collider2D>());
        }
        if (obj.CompareTag("StartCutscene"))
        {
            GameObject.Find("TimeLineManager").GetComponent<PlayableDirector>().Play();
            GameObject.Find("StartBossCutscene").SetActive(false);
        }
        if (obj.CompareTag("ArrowPickUp"))
        {
            arrowManager.GetComponent<ArrowManager>().AddArrow();
            Destroy(obj.gameObject);
        }
        if (obj.CompareTag("Dagger"))
        {
            obj.GetComponent<ItemPickup>().Pickup();
            hasDagger = true;
            holdingDagger = true;
        }
        if (obj.CompareTag("Crossbow"))
        {
            obj.GetComponent<ItemPickup>().Pickup();
            hasCrossbow = true;
        }
        if (obj.CompareTag("Item"))
        {
            obj.GetComponent<ItemPickup>().Pickup();
        }   
        
    }
   
}
