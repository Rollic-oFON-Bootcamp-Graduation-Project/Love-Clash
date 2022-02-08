using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

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
        Observer.StackHandleFinal += HandleFinalPath;
    }
    private void OnDisable()
    {
        Observer.AddToStack -= AddToStack;
        Observer.RemoveFromStack -= RemoveFromStack;
        Observer.StackHandleBattle -= HandleBattlePositions;
        Observer.StackHandleFinal -= HandleFinalPath;
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
        if (stack.Count == 0 || (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY && 
            GameManager.Instance.CurrentGameState != GameState.FINAL)) return;
        stack[StackCount - 1].transform.position = Vector3.Lerp(transform.position + offsetFirst, playerSideMovementRoot.transform.position, 0.8f);
        for (int i = StackCount - 2; i >= 0; i--)
        {
            stack[i].transform.position = Vector3.Lerp(stack[i + 1].transform.position + offset, stack[i].transform.position, 0.8f);
        }
    }
    private void HandleFinalPath(List<Vector3> finalPositions)
    {
        //Check if there are more collectables in stack than positions
        if(stack.Count != 0)
        {
            if (finalPositions.Count - 1 < stack.Count) return;

            SetFinalPath(finalPositions);
        }
        else
        {
            GameManager.Instance.GameOver();
        } 
    }
    private bool CheckIfNumberIsTriangular(int n)
    {
        var value = (8 * n) + 1;

        for(int i=1; i*i < value; i++)
        {
            if((value % i == 0) && (value / i == i))
            {
                return true;
            }
        }
        return false;
    }
    private int ClosestRoot(int n)
    {
        return Mathf.FloorToInt((Mathf.Sqrt((8 * n + 1)) - 1) / 2);
    }
    private int TriangleNumber(int n)
    {
        return (n * (n + 1)) / 2;
    }
    public void CollectableSetFinalPosition(Vector3 newPos)
    {
        StartCoroutine(CollectableSetFinalPositionRoutine(newPos));

    }

    private IEnumerator CollectableSetFinalPositionRoutine(Vector3 newPos)
    {
        var index = stack.Count - 1;
        var collectable = stack[index];
        collectable.CollectableParticle.UpdateParticle(ParticleType.FINAL);
        collectable.DisableCollider();
        stack.RemoveAt(index);
        yield return new WaitForSeconds(0.1f);
        collectable.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        collectable.transform.position = newPos;
        collectable.CollectableVisual.UpdateAnimState(MaleAnimState.FINAL);
        
    }
    private void SetFinalPath(List<Vector3> positions)
    {
        List<Vector3> path = new List<Vector3>();
        //Vector3[][] collectablePath = new Vector3[][] { };
        int collectableCount = stack.Count;
        if (!CheckIfNumberIsTriangular(stack.Count))
        {
            collectableCount = TriangleNumber(ClosestRoot(stack.Count));
        }

        //STATE
        for (int i = 0; i < collectableCount; i++)
        {
           //for(int k = 0; k < i; k++)
           //{
           //    for (int l = 0; l <= k;l++)
           //    {
           //        collectablePath[k][l] = positions[i];
           //    }   
           //}
            path.Add(positions[i]);
            //collectableCount++;
        }

        //path.Reverse();
        //Debug.Log(collectablePath);
        Observer.HandlePlayerFinalPath?.Invoke(path);
    }
    private void HandleBattlePositions(List<Vector3> positions, List<Collectable> collectables)
    {
        SetCollectablePositions(positions, collectables);
        //SET POSITIONS OF COLLECTABLES BY USING POISSON DISC SAMPLING
        //AND SEND IT AS A VECTOR3 LIST TO THE PLAYERSTACK TO HANDLE COLLECTABLE'S POSITIONS
    }
    private void SetCollectablePositions(List<Vector3> positions, List<Collectable> collectables)
    {
        try
        {

            for (int i = stack.Count - 1; i >= 0; i--)
            {
                stack[i].DisableCollider();
                StartCoroutine(MoveToPosition(stack[i], positions[i]));
                collectables.Add(stack[i]);
                stack.RemoveAt(i);
            }

        }
        catch (System.IndexOutOfRangeException ex)
        {
            throw new System.ArgumentException("Index is out of range", nameof(positions.Count), ex);
        }
    }
    private IEnumerator MoveToPosition(Collectable collectable, Vector3 newPos)
    {
        Debug.Log("start");
        collectable.DisableCollider();
        yield return collectable.transform.DOMove(newPos, 2f)
                .OnComplete(() => OnCompleteMoving(collectable))
                .WaitForCompletion();
    }
    private void OnCompleteMoving(Collectable collectable)
    {
        Debug.Log("complete");
        collectable.CollectableVisual.UpdateAnimState(MaleAnimState.ONBATTLE);
        collectable.EnableCollider();
        collectable.IsCollected = false;
        GameManager.Instance.CollectablesAreReady();
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
        GameManager.Instance.CanEnterBattle = StackCount == 0 ? false : true;
        ActiveObstacleHitReaction();
        return collectable;
        
    }
    private void AddToStack(Collectable collectable)
    {
        GameManager.Instance.CanEnterBattle = true;
        stack.Add(collectable);
    }
    private void ActiveObstacleHitReaction()
    {
        foreach (Collectable collectable in stack)
        {
            collectable.CollectableVisual.UpdateAnimState(MaleAnimState.HITREACTION);
        }
    }
}
