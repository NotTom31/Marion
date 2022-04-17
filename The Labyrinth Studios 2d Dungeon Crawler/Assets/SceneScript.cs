using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour , IDataPersistence
{

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
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
