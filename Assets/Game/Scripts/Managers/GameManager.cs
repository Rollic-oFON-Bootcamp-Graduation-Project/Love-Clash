using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState CurrentGameState;
    private void Awake()
    {
        CurrentGameState = GameState.MENU;
    }

    public void StartGame()
    {
        UIManager.Instance.StartScreen.DisablePanel();
        CurrentGameState = GameState.GAMEPLAY;
    }

    public void StartBattle()
    {
        //SET POSITIONS OF COLLECTABLES BY USING POISSON DISC SAMPLING
        //AND SEND IT AS A VECTOR3 ARRAY TO THE PLAYERSTACK TO HANDLE COLLECTABLE'S POSITIONS
        CurrentGameState = GameState.BATTLE;
    }

}
