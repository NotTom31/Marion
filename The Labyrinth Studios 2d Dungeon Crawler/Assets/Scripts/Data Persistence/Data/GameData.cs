using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/*Okay, so this script will be the data that we actually want to store, whether it be the player's health, position, inventory, you name it
 in order to fully implement this class, one would need to also implement Data Persistence Interface into the scripts in which the desired data is being
 stored*/
[System.Serializable]
public class GameData
{
    public int currentHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> leversActivated;
    public GameData()//New Game values will be stored in this method
    {
        this.currentHealth = 3;//current health is loaded and saved in the player script.
        playerPosition = Vector3.zero;//player position is loaded and saved in the player script

        //for things like levers, we use the string to create a Guid and the bool for whether or not its been activated. 
        leversActivated = new SerializableDictionary<string, bool>();//levers activated are loaded and saved in the lever script
    }
}

