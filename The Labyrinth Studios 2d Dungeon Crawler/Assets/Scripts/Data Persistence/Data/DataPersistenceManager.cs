using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
/*This script is the one doing the work in saving and loading data*/
public class DataPersistenceManager : MonoBehaviour
{
    //Allows us to name the file in the Inspector
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;//GameData variable, see GameData script
    private List<IDataPersistence> dataPersistenceObjects;//List that holds the save and load functions
    private FileDataHandler dataHandler;//FileDataHandler variable, see FileDataHandler script
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)//check to make sure there already isn't another instance, there shouldn't be
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
            Destroy(this.gameObject);
            return;
        }
        instance = this;//creates the manager
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
    
    public void NewGame()
    {
        this.gameData = new GameData();//uses the constructor in the GameData script
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
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }
        //pass data to other scripts to update
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        //save the data to a file using the data handler
        dataHandler.Save(gameData);
    }
    //Right now we are saving when the application quits, we want to save when we choose to save
    private void OnApplicationQuit()
    {
       SaveGame();
    }
    //this is the list where the game data will actually be stored.
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public bool HasGameData()
    {
        return gameData != null;
    }
}
