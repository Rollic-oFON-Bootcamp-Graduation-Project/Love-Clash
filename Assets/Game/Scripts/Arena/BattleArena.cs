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
        Observer.StartBattle -= StartShooting;
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
                Gizmos.DrawSphere(stackPoints[i], displayRadius);
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
            float timer = 0f;
            while (timer <= loveTimer)
            {
                //UPDATE COLLECTABLE BAR
                timer += Time.deltaTime;
                yield return null;
            }

            if (collectables.Contains(closestCollectable))
            {
                //closestCollectable.IsCollected = true;
                //closestCollectable.CollectableVisual.UpdateAnimState(MaleAnimState.HATE);
                //closestCollectable.CollectableParticle.UpdateParticle(ParticleType.HATE);
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
        //SET LIMIT TO OLD POS;
        StopCoroutine(battleRoutine);
        Destroy(gameObject);
    }

    private void SetCollectablePositions()
    {
        if (isTriggered) return;
        isTriggered = true;
        stackPoints = GenerateRandomPoints();
        Observer.StackHandleBattle?.Invoke(stackPoints, collectables);
        //Observer.UpdatePlayerLimit?.Invoke(-bottomLimit.localPosition.x, bottomLimit.localPosition.x);
        
    }

    private void StartShooting()
    {
        Debug.Log("arena started shooting");
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
            Observer.RemoveFromArena += RemoveFromArenaCollectables;
            Observer.StartBattle += StartShooting;
            Observer.PreBattle += SetCollectablePositions;
            Observer.StopBattle += StopBattle;
            GameManager.Instance.StartBattle(this);
        }
    }
}
