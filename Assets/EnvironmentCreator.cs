using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnvironmentCreator : MonoBehaviour
{
    //TODO
    [SerializeField] private GameObject myQuad;
    [SerializeField] private List<Vector3> envPoints;
    [SerializeField, Range(20, 100)] public float radius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    public float displayRadius = 1;
    //private Vector3 envSize => new Vector3(200, 1, RoadManager.Instance.RoadCount * 20f);
    //private void Start()
    //{
    //    //envSize = new Vector3(200, 1, RoadManager.Instance.RoadCount * 20f);
    //    myQuad.transform.localScale = envSize;
    //
    //    envPoints = PoissonDiscSampling.GeneratePoints(radius, envSize, out pointCount);
    //}
    //TODO
   //void OnDrawGizmos()
   //{
   //    var gizmoColor = Color.green;
   //    gizmoColor.a = 0.4f;
   //    Gizmos.color = gizmoColor;
   //    Gizmos.DrawWireCube(envSize / 2, envSize);
   //
   //    if (envPoints != null)
   //    {
   //        for (int i = 0; i < envPoints.Count; i++)
   //        {
   //            Gizmos.DrawSphere(envPoints[i], displayRadius);
   //        }
   //    }
   //}
}
