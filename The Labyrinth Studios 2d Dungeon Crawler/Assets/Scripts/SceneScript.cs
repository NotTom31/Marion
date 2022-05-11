using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour , IDataPersistence
{
    public static SceneScript instance { get; private set; }
    public void LoadData(GameData data)
    {
        this.currentScene = data.currentScene;
    }
    public void SaveData(GameData data)
    {
        data.currentScene = this.currentScene;
    }

    public string currentScene;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)//checks to see if another SceneScript object exists
        {
            Debug.Log("Found more than one Scene Data in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        this.currentScene = SceneManager.GetActiveScene().name;
    }
}
