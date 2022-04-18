using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    public static bool GameIsOver = false;

    public GameObject gameOverMenuUI;

    // Update is called once per frame
      public void GameStatus()
    {
        Death();
        Debug.Log("Loading game over screen...");
    }


    public void Alive()
    {
        gameOverMenuUI.SetActive(false);  //hides the menu
        Time.timeScale = 1f; //unfreeze time
        GameIsOver = false;
    }

    public void Death()
    {
        gameOverMenuUI.SetActive(true); //shows the death menu        
        Time.timeScale = 0f; //freeze time
        GameIsOver = true;
    }

    public void LoadMenu()
    {
        GameIsOver = false;
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
