using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoss : Enemy, IMoveable
{
    private int maxMinionAmount;
    private int currentMinionCount;
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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 90;
        maxMinionAmount = 10;
        currentMinionCount = 0;
        teleportCooldown = 15f;
        lastTeleport = 0f;
        upperLeftPosition = new Vector3(.22f, 3.49f, transform.position.z);
        lowerLeftPosition = new Vector3(.22f, 2.05f, transform.position.z);
        upperRightPosition = new Vector3(4.7f, 3.49f, transform.position.z);
        lowerRightPosition = new Vector3(4.7f, 2.05f, transform.position.z);

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
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        if (Time.time > (lastTeleport + teleportCooldown) && this.currentState != EnemyState.attack)
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
            Debug.Log(lastTeleport);
            this.currentState = EnemyState.idle;
        }
    }
    private void EnemySpawn()
    {

    }
    private IEnumerator TeleportCo(Vector3 teleportPosition)
    {
        this.currentState = EnemyState.teleport;
        bigRatFace.SetActive(true);
        StartCoroutine(SpawnEnemyCo(transform.position));
        transform.localPosition = disappear;
        yield return new WaitForSeconds(10f);        
        transform.localPosition = teleportPosition;
        bigRatFace.SetActive(false);
        this.currentState = EnemyState.idle;
    }
    private IEnumerator SpawnEnemyCo(Vector3 SpawnPosition)
    {
        while (currentMinionCount < maxMinionAmount)
        {
            Vector3 spawnHere = SpawnPosition;
            spawnHere.x = randNum.Next(-4, 1);
            spawnHere.y = randNum.Next(0, 3);
            summonRandNum = randNum.Next(1, 3);
            Debug.Log("akjshf;laksjfkj");
            if (summonRandNum == 1)
            {
                Instantiate(aLintEnemy, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                currentMinionCount++;
            }
            if (summonRandNum == 2)
            {
                Instantiate(aSpiderEnemy, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                currentMinionCount++;
            }
            if (summonRandNum == 3)
            {
                Instantiate(aRatPlagueDr, new Vector3(spawnHere.x, spawnHere.y, transform.position.z), Quaternion.identity);
                currentMinionCount++;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
