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
    [SerializeField] private GameObject noSavedGameDialog = null;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void LoadGameDialogYes()
    {
        DisableMenuButtons();
        // load the next scene - which will in turn load the game because of 
        // OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Level_1");

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
    
}
