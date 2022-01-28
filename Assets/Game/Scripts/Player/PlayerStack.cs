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
    private float firstStackGap => SettingsManager.FirstStackGap;
    private Vector3 offset;
    private Vector3 offsetFirst;

    private void OnEnable()
    {
        Observer.AddToStack += AddToStack;
        Observer.RemoveFromStack += RemoveFromStack;
        Observer.StackHandleBattle += HandleBattlePositions;
    }
    private void OnDisable()
    {
        Observer.AddToStack -= AddToStack;
        Observer.RemoveFromStack -= RemoveFromStack;
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
        offsetFirst = Vector3.back * firstStackGap;
    }
    private void FollowPlayer()
    {
        transform.position = playerSideMovementRoot.position + offset;
    }
    private void HandleStackMovement()
    {
        if (stack.Count == 0 || GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        stack[0].transform.position = Vector3.Lerp(transform.position + offsetFirst, playerSideMovementRoot.transform.position, 0.8f);
        for(int i = 1; i < stack.Count; i++)
        {
            stack[i].transform.position = Vector3.Lerp(stack[i - 1].transform.position + offset, stack[i].transform.position, 0.8f);
        }
    }

    private void HandleBattlePositions(List<Vector3> positions, List<Collectable> collectables)
    {
        GameManager.Instance.StartBattle();
        SetCollectablePositions(positions, collectables);
        
        
        //SET POSITIONS OF COLLECTABLES BY USING POISSON DISC SAMPLING
        //AND SEND IT AS A VECTOR3 LIST TO THE PLAYERSTACK TO HANDLE COLLECTABLE'S POSITIONS
    }

    private void SetCollectablePositions(List<Vector3> positions,  List<Collectable> collectables)
    {
        try
        {
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                stack[i].IsCollected = false;
                stack[i].transform.position = positions[i];
                //stack[i].CollectableVisual.BattleAnimation();
                collectables.Add(stack[i]);
                stack.RemoveAt(i);
            }

        }
        catch (System.IndexOutOfRangeException ex)
        {
            throw new System.ArgumentException("Index is out of range", nameof(positions.Count), ex);
        }
    }


    private Collectable RemoveFromStack()
    {
        Collectable collectable = null;
        if (stack.Count == 0) 
        {
            collectable = null;
            return null;
        }
        collectable = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        return collectable;
    }
    private void AddToStack(Collectable collectable)
    {
        stack.Add(collectable);
    }
}
