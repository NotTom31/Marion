using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : Collectable, IDataPersistence
{
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    public void LoadData(GameData data)
    {
        this.collected = data.keyCollected; 
        if (this.collected)
        {
            KeyCollect.Invoke();
        }
    }
    public void SaveData(GameData data)
    {
        data.keyCollected = this.collected;
    }

    public UnityEvent KeyCollect;

    protected override void OnCollect()
    {
        if (!collected) 
        {
            collected = true;
            KeyCollect.Invoke(); //runs the unity event to unlock whatever the key is linked to
            Debug.Log("Key Grab");
        }
    }    
}
