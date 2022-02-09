using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform playerSideMovementRoot;
    [SerializeField, ReadOnly] private List<Collectable> stack = new List<Collectable>();
    [SerializeField, ReadOnly] private Vector3[,] collectablePath = new Vector3[10, 10];


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
    private void HandleFinalPath(List<Vector3> finalPositions, int platformCount)
    {
        //Check if there are more collectables in stack than positions
        if(stack.Count != 0)
        {
            //if (finalPositions.Count - 1 < stack.Count) return;

            SetFinalPath(finalPositions, platformCount);
        }
        else
        {
            GameManager.Instance.GameOver();
        } 
    }
    public void SetFinalPosition(int index)
    {
        //StartCoroutine(TestSetFinalPositionRoutine(index));
        var finalCount = TriangularNumber.TriangleNumber(index + 1);
        for (int i = 0; i < index +1; i++)
        {
            var lastIndex = stack.Count - 1;
            var collectable = stack[lastIndex];
            collectable.CollectableParticle.UpdateParticle(ParticleType.FINAL);
            collectable.DisableCollider();
            stack.RemoveAt(lastIndex);
            collectable.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            collectable.transform.position = collectablePath[index, i];
            collectable.CollectableVisual.UpdateAnimState(MaleAnimState.FINAL);

        }
    }
    //OLD
    //public void CollectableSetFinalPosition(Vector3 newPos)
    //{
    //    StartCoroutine(CollectableSetFinalPositionRoutine(newPos));
    //
    //}
    //private IEnumerator CollectableSetFinalPositionRoutine(Vector3 newPos)
    //{
    //    var index = stack.Count - 1;
    //    var collectable = stack[index];
    //    collectable.CollectableParticle.UpdateParticle(ParticleType.FINAL);
    //    collectable.DisableCollider();
    //    stack.RemoveAt(index);
    //    yield return new WaitForSeconds(0.1f);
    //    collectable.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
    //    collectable.transform.position = newPos;
    //    collectable.CollectableVisual.UpdateAnimState(MaleAnimState.FINAL);
    //    
    //}
    private void SetFinalPath(List<Vector3> positions, int platformCount)
    {
        //Debug.Log($"{stack.Count}");
        List<Vector3> path = new List<Vector3>();
        int collectableCount = stack.Count;
        var finalCount = positions.Count - 1 < stack.Count ? platformCount -1 : TriangularNumber.ClosestRoot(stack.Count);
        //finalCount = finalCount >= platformCount ? platformCount : finalCount;
        if (!TriangularNumber.CheckIfNumberIsTriangular(stack.Count))
        {
            Debug.Log("it is not a triangular");
            collectableCount = TriangularNumber.TriangleNumber(finalCount);
        }
        Debug.Log($"{stack.Count} {finalCount}");
        int a = 0;
        for(int i = 0; i < finalCount; i++)
        {
            for(int j = 0; j <= i; j++)
            {
                //Debug.Log($"{i} {j} position = {positions[a]}" );
                collectablePath[i, j] = positions[a];
                path.Add(positions[a]);
                a++;
            }
        }
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
    public void DanceCollectables()
    {
        for(int i = 0; i < stack.Count; i++)
        {
            stack[i].CollectableVisual.UpdateAnimState(MaleAnimState.DANCE);
        }
    }
}
