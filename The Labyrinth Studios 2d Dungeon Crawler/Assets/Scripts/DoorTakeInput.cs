using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTakeInput : Collectable , IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] private string doorLockId;//will be used for saving game state
    [SerializeField] private string doorOpenId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        doorLockId = System.Guid.NewGuid().ToString();
        doorOpenId = System.Guid.NewGuid().ToString();
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    /*
     LoadData(GameData data)
     Accessor
     Method is used to l
     */
    public void LoadData(GameData data)
    {
        if (data.doorUnlocked.ContainsKey(doorLockId))
        {
            data.doorUnlocked.TryGetValue(doorLockId, out unLocked);
            if (unLocked)
            {
                UnlockDoor();
                anim.SetBool("DoorUnlock", true);
            }
        }
        if (data.doorOpened.ContainsKey(doorOpenId))
        {
            data.doorOpened.TryGetValue(doorOpenId, out collected);
            if (collected)
            {
                DoorOn.Invoke();
                ActivatePortal.Invoke();
                anim.SetBool("DoorOpen", true);
            }
        }
    }
    public void SaveData(GameData data)
    {
        if(data.doorUnlocked.ContainsKey(doorLockId))
        {
            data.doorUnlocked.Remove(doorLockId);
        }
        data.doorUnlocked.Add(doorLockId, unLocked);
        if(data.doorOpened.ContainsKey(doorOpenId))
        {
            data.doorOpened.Remove(doorOpenId);
        }
        data.doorOpened.Add(doorOpenId, collected);
    }
    public bool unLocked;
    public UnityEvent DoorOn;
    public UnityEvent DoorOff;
    public UnityEvent DisplayText;
    public UnityEvent CloseText;
    public UnityEvent ActivatePortal;
    private Animator anim;
    private void Awake()
    {
        anim = this.gameObject.GetComponentInParent<Animator>();       
    }

    protected override void OnCollect()
    {
        
        if (Input.GetKeyDown("e") && unLocked == true)
        {
            anim.SetBool("DoorUnlock", true);
            if (!collected)
            {
                collected = true;
                DoorOn.Invoke(); //runs the unity events to disable the door
                GameObject.Find("Door Open SFX").GetComponent<AudioSource>().Play();//plays open door sfx
                ActivatePortal.Invoke();
                anim.SetBool("DoorOpen", true);
            }
            else
            {
                collected = false;
                DoorOff.Invoke(); //enables the door
                ActivatePortal.Invoke();
                anim.SetBool("DoorOpen", false);
            }
        }
        else if (Input.GetKeyDown("e") && unLocked == false)
        {
            DoorLocked();
        }

    }

    public void DoorLocked()
    {
        if (KeyManager.instance.KeyCount >= 1 && unLocked == false)
        {
            UnlockDoor();
            KeyManager.instance.SubtractKey();         
            anim.SetBool("DoorUnlock", true);
        }
        else
            DisplayText.Invoke();
    }

    public void UnlockDoor()
    {
        unLocked = true;
        DoorOff.Invoke();
    }

    public void LockDoor()
    {
        unLocked = false;
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            Debug.Log("Im here.");
        }
    }
}