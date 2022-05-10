using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    public bool fix;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerAnim;
    public PlayableDirector director;
    GameObject thePlayer;

    private void Start()
    {
        thePlayer = GameObject.Find("player");
        playerAnim = playerAnimator.runtimeAnimatorController;
        fix = true;
    }
    void Update()
    {
        if (director.state == PlayState.Playing)
        {
            thePlayer.GetComponentInChildren<Animator>().enabled = false;
            thePlayer.GetComponent<Player>().enabled = false;
            fix = false;
            //playerAnimator.runtimeAnimatorController.Equals(null);       
        }
        if(director.state != PlayState.Playing && fix == false)
        {
            thePlayer.GetComponent<Player>().enabled = true;
            thePlayer.GetComponentInChildren<Animator>().enabled = true;
            //playerAnimator.runtimeAnimatorController.Equals(playerAnim);
            fix = true;
           
        }
    }
}
