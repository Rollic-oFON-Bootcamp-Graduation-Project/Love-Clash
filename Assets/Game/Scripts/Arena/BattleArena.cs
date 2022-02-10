using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using DG.Tweening;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private Transform upperLimit, bottomLimit, createCenter, lovePoint;
    [SerializeField] private BoxCollider arenaCollider;
    [SerializeField] private float loveTimer = 2f;
    [SerializeField] private List<Collectable> collectables;
    [SerializeField, Range(1, 10)] private float radius = 1f;
    [SerializeField] private float displayRadius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    [SerializeField] private Points playerPoints;
    private Coroutine battleRoutine;
    public Vector3 RegionSize = Vector3.one;

    public List<Transform> PlayerCollectablePositions => playerPoints.PointList;
    public List<bool> IsPositionTakenByCollectable => playerPoints.IsTaken;
    private Vector3 offset => createCenter.position;

    private bool isTriggered = false;
    private List<Vector3> stackPoints;
    private void OnDestroy()
    {
        Observer.RemoveFromArena -= RemoveFromArenaCollectables;
        Observer.PreBattle -= SetCollectablePositions;
        Observer.StopBattle -= StopBattle;
        Observer.StartBattle -= StartBattle;
    }

    private void Start()
    {
    }
    private void OnValidate()
    {
        stackPoints = PoissonDiscSampling.GeneratePoints(radius, RegionSize, out pointCount, offset);
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
                var drawOffset = stackPoints[i] - offset;
                Gizmos.DrawSphere(offset + drawOffset, displayRadius);
            }
        }
    }

    //THIS IS FOR REMOVING COLLECTABLES FOR PLAYER TO SHOOT/COLLECT
    private IEnumerator GetCollectableFromPlayer()
    {
        while (collectables.Count > 0)
        {
            //var closestCollectable = collectables.OrderBy(o => (lovePoint.transform.position - o.transform.position).sqrMagnitude).First();
            var closestCollectable = collectables[0];
            closestCollectable.CollectableUI.SetMaxLove(loveTimer);
            closestCollectable.CollectableUI.StartBattleUI();
            float timer = 0f;
            while (timer <= loveTimer)
            {
                
                //UPDATE COLLECTABLE BAR
                timer += Time.deltaTime;
                closestCollectable.CollectableUI.UpdateLoveBar(timer);
                if (!collectables.Contains(closestCollectable)) break;
                yield return null;
            }
            if (collectables.Contains(closestCollectable))
            {
                closestCollectable.TakenByEnemy(HitType.ARENA);
                closestCollectable.transform.SetParent(transform);
                collectables.Remove(closestCollectable);
            }
        }

        //STATE EXIT
        if (isTriggered && collectables.Count == 0)
        {
            //TODO

            GameManager.Instance.StopBattle();
        }
    }

    private void RemoveFromArenaCollectables(Collectable collectable)
    {
        if (collectables.Count == 0) return;
        collectables.Remove(collectable);
    }


    private void StopBattle()
    {
        arenaCollider.enabled = false;
        transform.DOScale(Vector3.zero, 1f)
            .SetEase(Ease.InBounce)
            .OnComplete(() => Destroy(gameObject));
        StopCoroutine(battleRoutine);
        //Destroy(gameObject);
    }

    private void SetCollectablePositions()
    {
        if (isTriggered) return;
        isTriggered = true;
        stackPoints = GenerateRandomPoints();
        Observer.StackHandleBattle?.Invoke(stackPoints, collectables);
        //Observer.UpdatePlayerLimit?.Invoke(-bottomLimit.localPosition.x, bottomLimit.localPosition.x);

    }
    private void StartBattle()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        battleRoutine = StartCoroutine(GetCollectableFromPlayer());
    }

    private List<Vector3> GenerateRandomPoints()
    {
        var generatedPoints = PoissonDiscSampling.GeneratePoints(radius, RegionSize, out pointCount, offset);
        generatedPoints = generatedPoints.OrderBy(o => (lovePoint.transform.position - o).sqrMagnitude).ToList();
        return generatedPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (!GameManager.Instance.CanEnterBattle)
            {
                //GameManager.Instance.CollectablesAreReady();
                //GameManager.Instance.GameOver();
                //return;
            }
            Observer.RemoveFromArena += RemoveFromArenaCollectables;
            Observer.StartBattle += StartBattle;
            Observer.PreBattle += SetCollectablePositions;
            Observer.StopBattle += StopBattle;
            GameManager.Instance.StartBattle(this);
        }
    }
}
