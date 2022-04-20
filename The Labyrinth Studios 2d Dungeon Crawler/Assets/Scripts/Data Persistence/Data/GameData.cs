using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
/*Okay, so this script will be the data that we actually want to store, whether it be the player's health, position, inventory, you name it
 in order to fully implement this class, one would need to also implement Data Persistence Interface into the scripts in which the desired data is being
 stored*/
[System.Serializable]
public class GameData
{
    public int currentHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> leversActivated;
    public bool plateOn;
    public bool keyCollected;
    public Vector3 boxPosition;
    public string currentScene;
    public GameData()//New Game values will be stored in this method
    {
        this.currentHealth = 3;//current health is loaded and saved in the player script.
        playerPosition = new Vector3(0.14f, -3.72f,0f);//player position is loaded and saved in the player script        
        leversActivated = new SerializableDictionary<string, bool>();//levers activated are loaded and saved in the lever script
        bool plateOn = false;//pressure plate in the on or off stated
        bool keyCollected = false;//keys that have been collected 
        boxPosition = new Vector3(-2.432f, -2.098f,0);//box position is loaded and saved in box script
        this.currentScene = "Level_1";//This is where the name of the current scene will be kept
    }
}

