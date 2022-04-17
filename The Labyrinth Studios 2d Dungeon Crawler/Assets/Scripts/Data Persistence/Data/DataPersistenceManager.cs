
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*This script is the one doing the work in saving and loading data*/
public class DataPersistenceManager : MonoBehaviour
{
    
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Application.persistentDataPath);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //Load any saved Data from a file using the data handler
        this.gameData = dataHandler.Load();


        //if no data to load, make a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }
        //push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    //this is the method that saves the game
    public void SaveGame()
    {
        //pass data to other scripts to update
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        //save the data to a file using the data handler
        dataHandler.Save(gameData);
    }
    //this should save the attributes we put in the list, but isn't working as intended
    private void OnApplicationQuit()
    {
       // SaveGame();
    }
    //this is the list where the game data will actually be stored.
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
