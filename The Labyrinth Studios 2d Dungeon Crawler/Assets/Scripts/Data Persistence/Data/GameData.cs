using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
/*This script will be the data that we actually want to store, whether it be the player's health, position, inventory, you name it
 In order to fully implement this class, one would need to also implement Data Persistence Interface into the scripts in which the desired data is being
 stored*/
[System.Serializable]
public class GameData
{
    public int currentHealth;
    public int keyCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> leversActivated;
    public SerializableDictionary<string, bool> doorUnlocked;
    public SerializableDictionary<string, bool> doorOpened;
    public SerializableDictionary<string, bool> plateOn;
    public bool keyCollected;
    public SerializableDictionary<string, Vector3> boxPosition;
    public string currentScene;
    public GameData()//New Game values will be stored in this method
    {
        this.currentHealth = 3;//current health is loaded and saved in the player script. **IN PLAYER.cs**
        this.keyCount = 0;//How many keys does the player current have **IN KEYMANAGER.cs**
        playerPosition = new Vector3(0.14f, -3.72f,0f);//player position is loaded and saved in the player script **IN PLAYER.cs** 
        leversActivated = new SerializableDictionary<string, bool>();//levers activated are loaded and saved in the lever script **IN LEVER.cs**
        doorUnlocked = new SerializableDictionary<string, bool>();//Doors that have been unlocked **IN DOORTAKEINPUT.cs**
        doorOpened = new SerializableDictionary<string, bool>();//Doors that have been opened, **IN DOORTAKEINPUT.cs**
        plateOn = new SerializableDictionary<string, bool>();//pressure plate in the on or off stated ** IN PRESSUREPLATE.cs**
        bool keyCollected = false;//keys that have been collected 
        boxPosition = new SerializableDictionary<string, Vector3>();//box's position is loaded and saved in box script
        this.currentScene = "Level_1";//This is where the name of the current scene will be kept
    }
}

