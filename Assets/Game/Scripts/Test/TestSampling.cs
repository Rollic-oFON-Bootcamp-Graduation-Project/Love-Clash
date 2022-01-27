using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSampling : MonoBehaviour
{

    [SerializeField] private Transform upperLimit, bottomLimit;
    public float radius = 1;
    public Vector3 regionSize = Vector3.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    List<Vector3> points;


    private void OnValidate()
    {
        //points = PoissonDiscSampling.GeneratePoints(radius, regionSize);
        //Debug.Log(points.Count);
    }
    void OnDrawGizmos()
	{
        var gizmoColor = Color.green;
        gizmoColor.a = 0.4f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(regionSize / 2, regionSize);

        if(points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.DrawSphere(points[i], displayRadius);
            }
        }
	}
}
