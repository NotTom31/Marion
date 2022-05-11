using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [ Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    private string buttonPressed;
    [SerializeField] private GameObject noSavedGameDialog = null;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    GameObject theLoadScene;
    private GameObject sceneHandle;
    private GameObject theData;
    private void Start()
    {
        theData = GameObject.Find("DataPersistenceManager");
        sceneHandle = GameObject.Find("Scene Data");
    }
    public void NewGameDialogYes()
    {
        buttonPressed = "New";
        StartCoroutine(chillOut(buttonPressed));        
    }

    public void LoadGameDialogYes()
    {
        buttonPressed = "Load";
        StartCoroutine(chillOut(buttonPressed));
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }
    private IEnumerator chillOut(string buttonName)
    {
        yield return new WaitForSeconds(2f);
        if(buttonName == "New")
        {
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadSceneAsync("Level_0");
        }
        if(buttonName == "Load")
        {
            SceneManager.LoadSceneAsync(sceneHandle.GetComponent<SceneScript>().currentScene);
        }        
    }
    
}
