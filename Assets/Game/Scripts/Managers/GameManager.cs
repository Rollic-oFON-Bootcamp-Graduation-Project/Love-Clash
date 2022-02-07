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
        Debug.Log("awake game manager");
    }
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }
    private IEnumerator StartGameRoutine()
    {
        CanEnterBattle = false;
        areCollectablesInPositions = false;
        CameraManager.Instance.SwitchCam("PlayerCam");
        UIManager.Instance.StartScreen.DisablePanel();
        UIManager.Instance.InGameScreen.EnablePanel();
        yield return new WaitForSeconds(2);
        CurrentGameState = GameState.GAMEPLAY;
        Observer.StartGame?.Invoke();
    }
    public void StartBattle(BattleArena arena)
    {
        CanEnterBattle = false;
        //CHANGE STATE TO BATTLE
        //CHANGE CAMERA POSITION
        //START ROUTINE UNTIL COLLECTABLES MOVES INTO THEIR POSITIONS THEN START THE BATTLE
        activeArena = arena;
        CurrentGameState = GameState.BATTLE;
        CameraManager.Instance.SwitchCam("BattleCam");
        StartCoroutine(StartBattleRoutine());
    }

    public void StartFinal()
    {
        CurrentGameState = GameState.FINAL;
        Observer.PreFinal?.Invoke();
    }
    public void Win()
    {
        Observer.StopBattle = null;
        activeArena = null;
        areCollectablesInPositions = false;
        CurrentGameState = GameState.FINAL;
        UIManager.Instance.InGameScreen.DisablePanel();
        UIManager.Instance.WinScreen.EnablePanel();
    }
    public Vector3 GetAPointFromActiveArena()
    {
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
        activeArena = null;
        UIManager.Instance.InGameScreen.DisablePanel();
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
            //THIS WILL NOT WORK IF EVERY COLLECTABLE MOVES WITH DIFFERENT SPEED!!
            //THEY MUST REACH THEIR TARGETS AT THE SAME TIME!
            //BATTLE WILL START WHEN COLLECTABLES ARE IN POSITION
            if (areCollectablesInPositions) break;
            yield return null;
        }
        //BATTLE STARTING
        Observer.StartBattle?.Invoke();
    }

}
