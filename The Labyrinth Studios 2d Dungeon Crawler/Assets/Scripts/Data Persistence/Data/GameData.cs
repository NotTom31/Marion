using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/*Okay, so this script will be the data that we actually want to store, whether it be the player's health, position, inventory, you name it*/
[System.Serializable]
public class GameData
{
    public int currentHealth;
    public Vector3 playerPosition;
    public Dictionary<string, bool> leversActivated;
    public GameData()//New Game values will be stored in this method
    {
        this.currentHealth = 3;//initializing health
        playerPosition = Vector3.zero;
    }
}

