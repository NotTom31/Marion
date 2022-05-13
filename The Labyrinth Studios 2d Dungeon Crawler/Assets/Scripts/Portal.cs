using UnityEngine;


public class Portal : Collidable, IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] private string portalId;//will be used for saving game state
    [SerializeField] private string portalPosId;
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        portalId = System.Guid.NewGuid().ToString();
        portalPosId = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        if(data.portal.ContainsKey(portalId))
        {
            data.portal.TryGetValue(portalId, out portalUsed);
        }
        if (portalUsed)
        {
            this.portalUsed = false;
            if (data.playerPortalPosition.ContainsKey(portalPosId))
            {
                data.playerPortalPosition.TryGetValue(portalPosId, out thePosition);
                if (thePosition != Vector3.zero)
                {
                    GameObject temp = GameObject.Find("player");
                    Vector3 tempPos = thePosition;
                    tempPos.y += .4f;
                    temp.transform.position = tempPos;
                }
            }            
        }
    }
    public void SaveData(GameData data)
    {
        Debug.Log(this.portalId);
            thePosition = transform.position;
            if (data.portal.ContainsKey(portalId))
            {
                data.portal.Remove(portalId);
            }
            data.portal.Add(portalId, portalUsed);
            if(data.playerPortalPosition.ContainsKey(portalPosId))
            {
                data.playerPortalPosition.Remove(portalPosId);
            }
            data.playerPortalPosition.Add(portalPosId, thePosition);            
        
    }
    private GameObject theData;
    protected Vector3 thePosition;
    private bool portalUsed;
     public void IfSaveClicked()
    {
        portalUsed = false;
    }
    



    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "player")
        {
            // Teleport the player
            this.portalUsed = true;
            GameObject temp = GameObject.Find("player");
            temp.GetComponent<Player>().usedAPortal = true;

            theData = GameObject.Find("DataPersistenceManager");
            theData.GetComponent<DataPersistenceManager>().SaveGame();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
