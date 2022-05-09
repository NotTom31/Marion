using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Collectable , IDataPersistence
{
    [SerializeField] public string chestId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        chestId = System.Guid.NewGuid().ToString();
    }
    public void LoadData(GameData data)
    {
        if (data.chestOpened.ContainsKey(chestId))
        {
            data.chestOpened.TryGetValue(chestId, out collected);
            if (collected)
            {
                ChestOpened.Invoke();
                GetComponent<SpriteRenderer>().sprite = emptyChest;
                HasKey = false;
            }
        }
    }
    public void SaveData(GameData data)
    {
        if(data.chestOpened.ContainsKey(chestId))
        {
            data.chestOpened.Remove(chestId);
        }
        data.chestOpened.Add(chestId, collected);
    }
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public bool HasKey = false;
    public UnityEvent ChestOpened;
    public UnityEvent KeyAnimation;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            ChestOpened.Invoke();
            GameObject.Find("Door Open SFX").GetComponent<AudioSource>().Play();//I thought this might sound good for the chest but we can change it 
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + pesosAmount + " pesos!");
            if (HasKey == true)
            {
                KeyManager.instance.AddKey();
                KeyAnimation.Invoke();
            }
        }
    }
    
}
