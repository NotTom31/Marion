using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TheGameStatus
{
    NewGame, LoadGame
}
public class GameStatus : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    TheGameStatus TheGameStatus;
   
    public void NewGameStatus()
    {
        TheGameStatus = TheGameStatus.NewGame;
    }
    public void LoadGameStatus()
    {
        TheGameStatus = TheGameStatus.LoadGame;
    }
}
