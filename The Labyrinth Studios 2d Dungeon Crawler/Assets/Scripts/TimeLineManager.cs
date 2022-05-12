using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    public bool fix;
    public bool on;
    public PlayableDirector director;
    GameObject thePlayer;
    GameObject theMusic;
    GameObject theIntroMusic;
    private void Start()
    {
        thePlayer = GameObject.Find("player");
        fix = true;
        on = false;
        theMusic = GameObject.Find("Boss theme (1)");
        theIntroMusic = GameObject.Find("Boss theme Intro");
    }
    void Update()
    {
        if (director.state == PlayState.Playing)
        {
            thePlayer.GetComponentInChildren<Animator>().enabled = false;
            thePlayer.GetComponent<Player>().enabled = false;
            fix = false;
            if(on == false)
            {
                theIntroMusic.GetComponent<AudioSource>().Play();
                on = true;
            }
        }
        if(director.state != PlayState.Playing && fix == false)
        {
            thePlayer.GetComponent<Player>().enabled = true;
            thePlayer.GetComponentInChildren<Animator>().enabled = true;
            fix = true;
            theMusic.GetComponent<AudioSource>().Play();
            GameObject.Find("RatPlagueDrBoss").GetComponent<RatBoss>().fightStarted = true;
            GameObject.Find("Portal").SetActive(false);
        }
    }
}
