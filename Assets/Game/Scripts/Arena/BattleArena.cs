using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private Transform upperLimit, bottomLimit, createCenter;
    [SerializeField] private List<Collectable> collectables;
    [SerializeField, Range(1,10)] private float radius = 1f;
    [SerializeField] private float displayRadius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    public Vector3 regionSize = Vector3.one;
    
    private Vector3 offset => createCenter.position;
    
    private bool isTriggered = false;
    private List<Vector3> points;

    private void OnValidate()
    {
        points = PoissonDiscSampling.GeneratePoints(radius,regionSize, out pointCount, offset);
    }

    private void OnDrawGizmos()
    {
        var gizmoColor = Color.red;
        gizmoColor.a = 0.8f;
        Gizmos.color = gizmoColor;

        var center = Vector3.Lerp(upperLimit.position, bottomLimit.position, 0.5f);
        center.y = 0.6f;
        Gizmos.DrawWireCube(center, regionSize);

        if (points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawSphere(points[i], displayRadius);
            }
        }
    }

    private void EndBattle()
    {
        //SET LIMIT TO OLD POS;
    }

    private List<Vector3> GenerateRandomPoints()
    {
        var generatedPoints = PoissonDiscSampling.GeneratePoints(radius, regionSize, out pointCount, offset);
        return generatedPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTriggered) return;
            isTriggered = true;
            points = GenerateRandomPoints();
            Observer.StackHandleBattle?.Invoke(points, collectables);
            Observer.UpdatePlayerLimit?.Invoke(-bottomLimit.localPosition.x, bottomLimit.localPosition.x);

        }
    }
}
