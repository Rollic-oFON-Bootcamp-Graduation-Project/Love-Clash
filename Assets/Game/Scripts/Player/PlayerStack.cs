using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform playerSideMovementRoot;
    [SerializeField, ReadOnly] private List<Collectable> stack = new List<Collectable>();
    public int StackCount => stack.Count;
    private float stackGap => SettingsManager.StackGap;
    private Vector3 offset;

    private void OnEnable()
    {
        Observer.AddToStack += AddToStack;
        Observer.StackHandleBattle += HandleBattlePositions;
    }
    private void OnDisable()
    {
        Observer.AddToStack -= AddToStack;
        Observer.StackHandleBattle -= HandleBattlePositions;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOffSet();
        FollowPlayer();
        HandleStackMovement();
    }
    private void UpdateOffSet()
    {
        offset = Vector3.back * stackGap;
    }
    private void FollowPlayer()
    {
        transform.position = playerSideMovementRoot.position + offset;
    }
    private void HandleStackMovement()
    {
        if (stack.Count == 0 || GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        stack[0].transform.position = Vector3.Lerp(transform.position + offset, playerSideMovementRoot.transform.position, 0.8f);
        for(int i = 1; i < stack.Count; i++)
        {
            stack[i].transform.position = Vector3.Lerp(stack[i - 1].transform.position + offset, stack[i].transform.position, 0.8f);
        }
    }

    private void HandleBattlePositions(Vector3 upperLimit, Vector3 bottomLimit)
    {
        GameManager.Instance.StartBattle();
        SetCollectablePositions(upperLimit, bottomLimit);
        //SET POSITIONS OF COLLECTABLES BY USING POISSON DISC SAMPLING
        //AND SEND IT AS A VECTOR3 ARRAY TO THE PLAYERSTACK TO HANDLE COLLECTABLE'S POSITIONS
        //WILL USE POISSON DISC SAMPLING HERE BUT NOW IT'S ALL RANDOM
    }

    private void SetCollectablePositions(Vector3 upperLimit, Vector3 bottomLimit)
    {
        for(int i = 0; i < stack.Count; i++)
        {
            var pos = Vector3.Lerp(bottomLimit, upperLimit, Random.value);
            stack[i].IsCollected = false;
            stack[i].transform.position = pos;
        }
        
    }

    private void AddToStack(Collectable collectable)
    {
        stack.Add(collectable);
    }
}
