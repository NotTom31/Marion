using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour, IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] public string pressurePlateId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        pressurePlateId = System.Guid.NewGuid().ToString();
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    public void LoadData(GameData data)
    {
        if (data.plateOn.ContainsKey(pressurePlateId))
        {
            data.plateOn.TryGetValue(pressurePlateId, out plateOn);
            if (plateOn)
            {
                this.GetComponent<Animator>().SetBool("on", true);
            }
        }
    }
    public void SaveData(GameData data)
    {
        if(data.plateOn.ContainsKey(pressurePlateId))
        {
            data.plateOn.Remove(pressurePlateId);
        }
        data.plateOn.Add(pressurePlateId, plateOn);
    }

    //******************************************************************************************************************************************************
    //********************************************************PLAYER CLASS ATTRIBUTES***********************************************************************
    //******************************************************************************************************************************************************
    public UnityEvent PlatePushed;
    public UnityEvent PlateReleased;
    public bool plateOn;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (!plateOn && obj.CompareTag("Box"))
        {
            this.GetComponent<Animator>().SetBool("on", true);
            plateOn = true;
        }        
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (plateOn && obj.CompareTag("Box"))
        {
            this.GetComponent<Animator>().SetBool("on", false);
            plateOn = false;
        }
    }
}