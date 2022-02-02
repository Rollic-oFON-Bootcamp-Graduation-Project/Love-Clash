using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;


public class PointCreator : MonoBehaviour
{
    [SerializeField] private Points pointParent;
    [SerializeField] private Transform topPoint;
    [SerializeField, Range(20,100)] public float radius = 1;
    public Vector3 regionSize = Vector3.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    GameObject newObj => new GameObject("Point");

    List<Vector3> points;


    private void OnValidate()
    {
        points = PoissonDiscSampling.GeneratePoints(radius, regionSize, out pointCount);
    }

    [Button]
    private void CreatePoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            var node = new GameObject().transform;
            node.SetParent(pointParent.transform);
            node.position = points[i];
            pointParent.PointList.Add(node);
            pointParent.IsTaken.Add(false);
        }

        pointParent.PointList = pointParent.PointList.OrderBy(o => (topPoint.transform.position - o.position).sqrMagnitude).ToList();
    }
    void OnDrawGizmos()
    {
        var gizmoColor = Color.green;
        gizmoColor.a = 0.4f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(regionSize / 2, regionSize);

        if (points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawSphere(points[i], displayRadius);
            }
        }
    }
}
