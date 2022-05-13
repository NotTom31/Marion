using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : Collectable , IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] private string boxId;//will be used for saving game state
    [SerializeField] private string boxHeldId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        boxId = System.Guid.NewGuid().ToString();
        boxHeldId = System.Guid.NewGuid().ToString();
    }
    //******************************************************************************************************************************************************
    //************************************************************DECLARING IDATAPERSISTENCE****************************************************************
    //******************************************************************************************************************************************************
    /*These methods load and savedata will be used to keep track of what levers have been activated*/
    public void LoadData(GameData data)
    {
        this.LastFacingHorizontal = data.LastHorizontalBox;
        this.LastFacingVertical = data.LastVerticalBox;
        if (data.boxPosition.ContainsKey(boxId))
        {
            data.boxPosition.TryGetValue(boxId, out currentPosition);
            if (currentPosition != Vector3.zero)
            {
                this.transform.position = currentPosition;
            }
        }
        if (data.boxPosition.ContainsKey(boxHeldId))
        {
            data.boxHeld.TryGetValue(boxHeldId, out collected);
            if (collected)
            {
                BoxLift.Invoke(); //attaches the box to the player
                BoxLayer.Invoke(); //initial setting the boxes local position and layer to keep it consistant
            }
        }
    }
    public void SaveData(GameData data)
    {
        data.LastHorizontalBox = this.LastFacingHorizontal;
        data.LastVerticalBox = this.LastFacingVertical;
        currentPosition = this.transform.position;
        if (data.boxPosition.ContainsKey(boxId))
        {
            data.boxPosition.Remove(boxId);
        }
        data.boxPosition.Add(boxId, currentPosition);
        if(data.boxHeld.ContainsKey(boxHeldId))
        {
            data.boxHeld.Remove(boxHeldId);
        }
        data.boxHeld.Add(boxHeldId, collected);
    }
//******************************************************************************************************************************************************
//***********************************************************BOX CLASS ATTRIBUTES***********************************************************************
//******************************************************************************************************************************************************
public UnityEvent BoxLift;
    public UnityEvent BoxLayer;
    public UnityEvent BoxLayerTwo;
    public float LastFacingHorizontal = 0;
    public float LastFacingVertical = 0;
    protected Vector3 currentPosition;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            LastFacingHorizontal = Input.GetAxisRaw("Horizontal");
            LastFacingVertical = Input.GetAxisRaw("Vertical");
        }
    }

    protected override void OnCollect()
    {
        if (collected)
        {
            BoxFacing();
        }
        if (Input.GetKeyDown("e")) //interact button
        {
            if (!collected)
            {
                collected = true;
                BoxLift.Invoke(); //attaches the box to the player
                BoxLayer.Invoke(); //initial setting the boxes local position and layer to keep it consistant
                //transform.localPosition = new Vector3(0, 0, 0);
                BoxFacing();

            }
            else
            {
                BoxDrop();
            }
        }
    }

    protected void BoxDrop()
    {
        collected = false;

        if (Input.GetAxisRaw("Horizontal") > 0 || LastFacingHorizontal > 0)
        {
            transform.localPosition = new Vector3(0.1f, -0.2f, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 || LastFacingHorizontal < 0)
        {
            transform.localPosition = new Vector3(-0.1f, -0.2f, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Vertical") > 0 || LastFacingVertical > 0)
        {
            transform.localPosition = new Vector3(0, 0.1f, 0);
            BoxLayer.Invoke(); //sets the box behind the player
        }
        if (Input.GetAxisRaw("Vertical") < 0 || LastFacingVertical < 0)
        {
            transform.localPosition = new Vector3(0, -0.44f, 0);
            BoxLayerTwo.Invoke(); //sets the box in front of the player
        }
        BoxLayer.Invoke();
        transform.parent = null; //detatches the box
    }

    protected void BoxFacing() //checks if you're holding the box, and moves the box relative to the players local position based on button inputs
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            LastFacingHorizontal = Input.GetAxisRaw("Horizontal");
            LastFacingVertical = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || LastFacingHorizontal > 0)
        {
            transform.localPosition = new Vector3(0.1f, 0, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Horizontal") < 0 || LastFacingHorizontal < 0)
        {
            transform.localPosition = new Vector3(-0.1f, 0, 0);
            BoxLayer.Invoke();
        }
        if (Input.GetAxisRaw("Vertical") > 0 || LastFacingVertical > 0)
        {
            transform.localPosition = new Vector3(0, 0.1f, 0);
            BoxLayer.Invoke(); //sets the box behind the player
        }
        if (Input.GetAxisRaw("Vertical") < 0 || LastFacingVertical < 0)
        {
            transform.localPosition = new Vector3(0, -0.1f, 0);
            BoxLayerTwo.Invoke(); //sets the box in front of the player
        }
    }

}




