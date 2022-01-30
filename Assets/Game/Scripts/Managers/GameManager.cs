using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState CurrentGameState;
    private bool areCollectablesInPositions = false;
    private void Awake()
    {
        CurrentGameState = GameState.MENU;
    }

    public void StartGame()
    {
        UIManager.Instance.StartScreen.DisablePanel();
        CurrentGameState = GameState.GAMEPLAY;
        Observer.StartGame?.Invoke();
    }

    public void StartBattle()
    {
        //CHANGE STATE TO BATTLE
        //CHANGE CAMERA POSITION
        //START ROUTINE UNTIL COLLECTABLES MOVES INTO THEIR POSITIONS THEN START THE BATTLE
        CurrentGameState = GameState.BATTLE;
        CameraManager.Instance.SwitchCam("BattleCam");
        StartCoroutine(StartBattleRoutine());
        
    }

    public void CollectablesAreReady()
    {
        areCollectablesInPositions = true;
    }

    public void StopBattle()
    {
        areCollectablesInPositions = false;
        CurrentGameState = GameState.GAMEPLAY;
        CameraManager.Instance.SwitchCam("PlayerCam");
        Observer.StopBattle?.Invoke();
    }

    private IEnumerator StartBattleRoutine()
    {
        //PRE BATTLE
        Observer.ArenaSetPositions?.Invoke();
        while (true)
        {
            //BATTLE WILL START WHEN COLLECTABLES ARE IN POSITION
            if (areCollectablesInPositions) break;
            yield return null;
        }
        //BATTLE STARTING
        //Observer.ArenaStartShooting?.Invoke();
        //Observer.PlayerAnimationChange?.Invoke();
        //Observer.PlayerStartBattle?.Invoke();
        Observer.StartBattle?.Invoke();
    }

}
