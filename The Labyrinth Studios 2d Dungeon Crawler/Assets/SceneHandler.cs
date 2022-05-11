using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour, IDataPersistence
{
    public string theCurrentScene;
    public void LoadData(GameData data)
    {
        //theCurrentScene = data.currentScene;
    }
    public void SaveData(GameData data)
    {
        //data.currentScene = SceneManager.GetActiveScene().name;
    }
}
