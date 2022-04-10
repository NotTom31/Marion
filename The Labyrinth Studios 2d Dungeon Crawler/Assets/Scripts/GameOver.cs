using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsOver = false;

    public GameObject gameOverMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f")) //needs to be changed to if player 0 health 
        {
            if (GameIsOver)
            {
                Resume(); 
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        gameOverMenuUI.SetActive(false);  //hides the menu
        Time.timeScale = 1f; //unfreeze time
        GameIsOver = false;
    }

    void Pause()
    {
        gameOverMenuUI.SetActive(true); //shows the pause menu
        Time.timeScale = 0f; //freeze time
        GameIsOver = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void LoadGame() //sends you back to be the beginning of the game on game over
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Dungeon"); //we will probably change this to send to start of current level or a checkpoint depending on how difficult we want the game
        Debug.Log("Loading Dungeon...");
        GameIsOver = false;
    }
}
