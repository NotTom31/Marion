using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : Collectable , IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] private string leverId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        leverId = System.Guid.NewGuid().ToString();
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    /*These methods load and savedata will be used to keep track of what levers have been activated*/
    public void LoadData(GameData data)
    {
        data.leversActivated.TryGetValue(leverId, out collected);
        if (collected)
        {
            LeverIsOn();
        }
    }
    public void SaveData(GameData data)
    {
        if(data.leversActivated.ContainsKey(leverId))
        {
            data.leversActivated.Remove(leverId);
        }
        data.leversActivated.Add(leverId, collected);
    }
    //******************************************************************************************************************************************************
    //************************************************************LEVER CLASS ATTRIBUTES********************************************************************
    //******************************************************************************************************************************************************
    public Sprite onLever; 
    public Sprite offLever;
    public UnityEvent LeverOn;
    public UnityEvent LeverOff;
    public UnityEvent ActivatePortal;

    
    protected override void OnCollect()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!collected)
            {
                collected = true;
                LeverIsOn();
            }
            else
            {
                collected = false;
                LeverIsOff();
            }
        }
    }
    /*made these methods to make it easier for load and save data*/
   public void LeverIsOn()
    {
        GetComponent<SpriteRenderer>().sprite = onLever;
        LeverOn.Invoke(); //run unity events
        ActivatePortal.Invoke(); //activate a warp if we want
        transform.localScale = new Vector3(-1, 1, 1); //flip the sprite
    }
    public void LeverIsOff()
    {
        GetComponent<SpriteRenderer>().sprite = offLever; //if we ever get an off lever sprite this will change the sprite instead of mirroring it
        LeverOff.Invoke(); //run unity events
        ActivatePortal.Invoke();
        transform.localScale = Vector3.one; //flip the sprite
    }

}




