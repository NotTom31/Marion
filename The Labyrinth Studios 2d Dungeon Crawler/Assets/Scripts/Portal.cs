using UnityEngine;


public class Portal : Collidable, IDataPersistence
{
    //******************************************************************************************************************************************************
    //***********************************************************************GUID***************************************************************************
    //******************************************************************************************************************************************************
    [SerializeField] private string portalId;//will be used for saving game state
    [ContextMenu("Generate guid for id")]
    /*the context menu above uses the GenerateGuid() below to allow someone to generate a unique id for levers.
     All one has to do is click on a lever, expand the lever script in the inspector, right click the script and select
    Generate guid for id. This will create a unique id that will be used when the game's state is saved.*/
    private void GenerateGuid()
    {
        portalId = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        if(data.portal.ContainsKey(portalId))
        {
            data.portal.TryGetValue(portalId, out portalUsed);
        }
        if(portalUsed)
        {
            GameObject temp = GameObject.Find("player");
            Vector3 tempPos = this.transform.position;
            tempPos.y += .4f;
            temp.transform.position = tempPos;
        }
    }
    public void SaveData(GameData data)
    {
        if(data.portal.ContainsKey(portalId))
        {
            data.portal.Remove(portalId);
        }
        data.portal.Add(portalId, portalUsed);
    }
    private bool portalUsed = false;
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "player")
        {
            // Teleport the player
            portalUsed = true;
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
