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

    // Update is called once per frame
    void Update()
    {
        
    }
}
