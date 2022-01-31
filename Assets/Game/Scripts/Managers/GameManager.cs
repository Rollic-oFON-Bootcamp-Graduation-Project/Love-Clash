using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState CurrentGameState;
    public bool CanEnterBattle = false;
    private PlayerStack playerStack;
    private bool areCollectablesInPositions = false;
    
    [SerializeField] private BattleArena activeArena;
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

    public void StartBattle(BattleArena arena)
    {
        //CHANGE STATE TO BATTLE
        //CHANGE CAMERA POSITION
        //START ROUTINE UNTIL COLLECTABLES MOVES INTO THEIR POSITIONS THEN START THE BATTLE
        activeArena = arena;
        CurrentGameState = GameState.BATTLE;
        CameraManager.Instance.SwitchCam("BattleCam");
        StartCoroutine(StartBattleRoutine());
    }

    public Vector3 GetAPointFromActiveArena()
    {
        Debug.Log("Get a point");
        for (int i = 0; i < activeArena.PlayerCollectablePositions.Count; i++)
        {
            if (!activeArena.IsPositionTakenByCollectable[i])
            {
                activeArena.IsPositionTakenByCollectable[i] = true;
                return activeArena.PlayerCollectablePositions[i].position;
            }
        }
        return Vector3.zero;
    }
    public void SetActiveArena(BattleArena arena)
    {
        activeArena = arena;
    }


    public void CollectablesAreReady()
    {
        areCollectablesInPositions = true;
    }

    public void GameOver()
    {
        UIManager.Instance.GameOverScreen.EnablePanel();
        CurrentGameState = GameState.MENU;
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
        Observer.PreBattle?.Invoke();
        while (true)
        {
            //BATTLE WILL START WHEN COLLECTABLES ARE IN POSITION
            if (areCollectablesInPositions) break;
            yield return null;
        }
        //BATTLE STARTING
        Observer.StartBattle?.Invoke();
    }

}
