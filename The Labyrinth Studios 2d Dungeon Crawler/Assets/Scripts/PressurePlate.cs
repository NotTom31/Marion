using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : Collectable, IDataPersistence
{
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    public void LoadData(GameData data)
    {
        this.plateOn = data.plateOn;
        if (this.plateOn)
        {
            this.GetComponent<Animator>().SetBool("on", true);
        }
    }
    public void SaveData(GameData data)
    {
        data.plateOn = this.plateOn;
        Debug.Log(data.plateOn);
    }

    //******************************************************************************************************************************************************
    //********************************************************PLAYER CLASS ATTRIBUTES***********************************************************************
    //******************************************************************************************************************************************************
    public UnityEvent PlatePushed;
    public UnityEvent PlateReleased;
    public bool plateOn;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (!plateOn)
        {
            this.GetComponent<Animator>().SetBool("on", true);
            plateOn = true;
        }        
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (plateOn)
        {
            this.GetComponent<Animator>().SetBool("on", false);
            plateOn = false;
        }
    }
}
