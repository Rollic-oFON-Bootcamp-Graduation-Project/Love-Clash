using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private Transform upperLimit, bottomLimit, createCenter, lovePoint;
    [SerializeField] private float loveTimer = 2f;
    [SerializeField] private List<Collectable> collectables;

    [SerializeField, Range(1,10)] private float radius = 1f;
    [SerializeField] private float displayRadius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    private Coroutine battleRoutine;
    public Vector3 RegionSize = Vector3.one;
    
    private Vector3 offset => createCenter.position;
    
    private bool isTriggered = false;
    private List<Vector3> stackPoints;

    private void OnEnable()
    {
        Observer.RemoveFromArena += RemoveFromArenaCollectables;
    }
    private void OnDisable()
    {
        Observer.RemoveFromArena -= RemoveFromArenaCollectables;
    }
    private void OnValidate()
    {
        stackPoints = PoissonDiscSampling.GeneratePoints(radius,RegionSize, out pointCount, offset);
    }

    private void OnDrawGizmos()
    {
        var gizmoColor = Color.red;
        gizmoColor.a = 0.8f;
        Gizmos.color = gizmoColor;

        var center = Vector3.Lerp(upperLimit.position, bottomLimit.position, 0.5f);
        center.y = 0.6f;
        Gizmos.DrawWireCube(center, RegionSize);

        if (stackPoints != null)
        {
            for (int i = 0; i < stackPoints.Count; i++)
            {
                Gizmos.DrawSphere(stackPoints[i], displayRadius);
            }
        }
    }

    //THIS IS FOR REMOVING COLLECTABLES FOR PLAYER TO SHOOT/COLLECT
    private IEnumerator GetCollectableFromPlayer()
    {
        while (collectables.Count > 0)
        {
            var closestCollectable = collectables.OrderBy(o => (lovePoint.transform.position - o.transform.position).sqrMagnitude).First();
            float timer = 0f;
            while (timer <= loveTimer)
            {
                //UPDATE COLLECTABLE BAR
                timer += Time.deltaTime;
                yield return null;
            }

            if (collectables.Contains(closestCollectable))
            {
                closestCollectable.IsCollected = true;
                closestCollectable.CollectableVisual.BattleAnimation();
                collectables.Remove(closestCollectable);
            }
        }
    }

    private void RemoveFromArenaCollectables(Collectable collectable)
    {
        if (collectables.Count == 0) return;
        collectables.Remove(collectable);
    }


    private void EndBattle()
    {
        //SET LIMIT TO OLD POS;
        StopCoroutine(battleRoutine);
    }

    private void StartBattle()
    {
        if(isTriggered) return;
        isTriggered = true;
        stackPoints = GenerateRandomPoints();
        Observer.StackHandleBattle?.Invoke(stackPoints, collectables);
        Observer.UpdatePlayerLimit?.Invoke(-bottomLimit.localPosition.x, bottomLimit.localPosition.x);
        battleRoutine = StartCoroutine(GetCollectableFromPlayer());
    }

    private List<Vector3> GenerateRandomPoints()
    {
        var generatedPoints = PoissonDiscSampling.GeneratePoints(radius, RegionSize, out pointCount, offset);
        return generatedPoints;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartBattle();
        }
    }
}
