using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatBoss : Enemy, IMoveable
{
    public bool fightStarted;
    private int maxMinionAmount;
    public int currentMinionAmount;
    private float teleportCooldown;//this will be used in conjuction with lastSummon to control how fast the boss teleports
    private float lastTeleport;
    //These will be used to store the positions where the boss will teleport
    private Vector3 upperLeftPosition;
    private Vector3 upperRightPosition;
    private Vector3 lowerLeftPosition;
    private Vector3 lowerRightPosition;
    private Vector3 disappear;
    protected int teleportNum;
    protected int summonRandNum;
    System.Random randNum = new System.Random();
    private GameObject bigRatFace;
    public GameObject aLintEnemy;
    public GameObject aSpiderEnemy;
    public GameObject aRatPlagueDr;

    private GameObject theUICamera;
    private Vector2 faceDir;
    protected List<GameObject> minionContainer;
    // Start is called before the first frame update
    void Start()
    {
        fightStarted = false;
        currentHealth = 45;
        maxMinionAmount = 5;
        teleportCooldown = 10f;
        lastTeleport = 0f;
        upperLeftPosition = new Vector3(-4.034f, 5.665f, transform.position.z);
        lowerLeftPosition = new Vector3(-4.034f, 1.11f, transform.position.z);
        upperRightPosition = new Vector3(4.05f, 5.665f, transform.position.z);
        lowerRightPosition = new Vector3(4.05f, 1.11f, transform.position.z);
        disappear = new Vector3(1000, 1000,transform.position.z);
        //-----------------------------
        /*Attributes from Enemy script*/
        //-----------------------------
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy, this will be used to return to after chasing and for reviving
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component
        bigRatFace = GameObject.Find("RatTurnAround_0");
        bigRatFace.SetActive(false);

        theUICamera = GameObject.Find("Pointer");
        theUICamera.SetActive(false );
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        Move();
    }
    
    public void FacePlayer()
    {
        faceDir = transform.position - target.position;
        anim.SetFloat("moveX", -faceDir.x);
        anim.SetFloat("moveY", -faceDir.y);
    }
   
    public void Move()
    {
        if(fightStarted == false)
        {
            teleportCooldown = 0;
        }        
        if (Time.time > (lastTeleport + teleportCooldown) && this.currentState != EnemyState.attack && fightStarted == true)
        {
            teleportNum = randNum.Next(0, 100);
            if (teleportNum <= 24)
            {
                StartCoroutine(TeleportCo(upperLeftPosition));
            }
            if (teleportNum >= 25 && teleportNum <= 49)
            {
                StartCoroutine(TeleportCo(upperRightPosition));
            }
            if (teleportNum >= 50 && teleportNum <= 74)
            {
                StartCoroutine(TeleportCo(lowerLeftPosition));
            }
            if (teleportNum >= 75)
            {
                StartCoroutine(TeleportCo(lowerRightPosition));
            }
            lastTeleport = Time.time;
            this.currentState = EnemyState.idle;
            if (fightStarted == true)
            {
                teleportCooldown = 12;
            }
        }
    }
    private IEnumerator TeleportCo(Vector3 teleportPosition)
    {
        this.currentState = EnemyState.teleport;
        bigRatFace.SetActive(true);
        StartCoroutine(SpawnEnemyCo(transform.position));
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponentInChildren<BoxCollider2D>().enabled = false;
        theUICamera.SetActive(false);
        yield return new WaitForSeconds(10f);        
        transform.localPosition = teleportPosition;
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponentInChildren<BoxCollider2D>().enabled = true;
        bigRatFace.SetActive(false);
        this.currentState = EnemyState.idle;
        theUICamera.SetActive(true);
    }    
    private IEnumerator SpawnEnemyCo(Vector3 SpawnPosition)
    {
        while(currentMinionAmount< maxMinionAmount)
        {
            if (this.currentHealth >= 31)
            {
             summonRandNum = randNum.Next(1, 2);
            }
            if(this.currentHealth <= 30 && this.currentHealth >= 16)
            {
             maxMinionAmount = 8;
             summonRandNum = randNum.Next(1, 3);
            }
            if(this.currentHealth <= 15 && this.currentHealth > 0)
            {
                maxMinionAmount = 10;
                summonRandNum = randNum.Next(1, 4);
            }
       
            Vector3 spawnHere = SpawnPosition;
            spawnHere.x = randNum.Next(-3, 3);
            spawnHere.y = randNum.Next(1, 4);
            
            if (summonRandNum == 1)
            {
                GameObject newEnemy = Instantiate(aLintEnemy, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                newEnemy.GetComponent<LintEnemy>().currentHealth = 2;
                newEnemy.tag = "BossSummon";
                newEnemy.GetComponent<Enemy>().chaseRadius = 50f;
            }
            if (summonRandNum == 2)
            {
                GameObject newEnemy = Instantiate(aSpiderEnemy, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                newEnemy.GetComponent<EnemyRanged>().currentHealth = 2;
                newEnemy.tag = "BossSummon";
                newEnemy.GetComponent<Enemy>().chaseRadius = 50f;
            }
            if (summonRandNum == 3)
            {
                GameObject newEnemy = Instantiate(aRatPlagueDr, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                newEnemy.GetComponent<RatEnemy>().currentHealth = 4;
                newEnemy.tag = "BossSummon";
                newEnemy.GetComponent<Enemy>().chaseRadius = 50f;
            }
            currentMinionAmount++;
            yield return new WaitForSeconds(.8f);
        }        
    }
}
