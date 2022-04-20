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
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level_1");
    }

    public void LoadGameDialogYes()
    {
        DisableMenuButtons();
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
