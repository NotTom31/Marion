using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBoss : Enemy, IMoveable
{
    private int maxMinionAmount;
    private int currentMinionCount;
    private float summonCooldown;//this will be used in conjunction with lastSummon to control how fast the boss summons more minions
    private float teleportCooldown;//this will be used in conjuction with lastSummon to control how fast the boss teleports
    private float lastSummon;//this will be used in conjunction with attCooldown and teleportCooldown to control how fast the boss does each
    private float lastTeleport;
    //These will be used to store the positions where the boss will teleport
    private Vector3 upperLeftPosition;
    private Vector3 upperRightPosition;
    private Vector3 lowerLeftPosition;
    private Vector3 lowerRightPosition;
    private Vector3 currentPosition;
    protected int teleportNum;
    protected int timeTracker;
    System.Random randNum = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 90;
        maxMinionAmount = 10;
        currentMinionCount = 0;
        summonCooldown = 5f;
        teleportCooldown = 5f;
        lastSummon = 0f;
        lastTeleport = 0f;
        upperLeftPosition = new Vector3(.22f, 3.49f, transform.position.z);
        lowerLeftPosition = new Vector3(.22f, 2.05f, transform.position.z);
        upperRightPosition = new Vector3(4.7f, 3.49f, transform.position.z);
        lowerRightPosition = new Vector3(4.7f, 2.05f, transform.position.z);
        currentPosition = new Vector3(0, 0, 0);
        //-----------------------------
        /*Attributes from Enemy script*/
        //-----------------------------
        target = GameObject.FindWithTag("Player").transform;//stores the player as the target
        homePosition = this.gameObject.transform.position;//stores the home position of the enemy, this will be used to return to after chasing and for reviving
        anim = this.GetComponent<Animator>();//Initializes the animator component
        thisBody = this.GetComponent<Rigidbody2D>();//Initializes the Rigidbody2d component
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
            this.currentState = EnemyState.walk;
            teleportNum = randNum.Next(0, 100);
            if (teleportNum <= 24)
            {
                Debug.Log("should be the Upper Left");
                transform.localPosition = upperLeftPosition;
            }
            if (teleportNum >= 25 && teleportNum <= 49)
            {
                Debug.Log("should be the Upper Right");
                transform.localPosition = upperRightPosition;
            }
            if (teleportNum >= 50 && teleportNum <= 74)
            {
                Debug.Log("should be the Lower Left");
                transform.localPosition = lowerLeftPosition;
            }
            if (teleportNum >= 75)
            {
                Debug.Log("should be the Lower Right");
                transform.localPosition = lowerRightPosition;
            }
            lastTeleport = Time.time;
            this.currentState = EnemyState.idle;
        }        
    }
}
