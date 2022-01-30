using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public List<Vector3> PointList;
    public List<bool> IsTaken;
    [SerializeField] private float displayRadius = 1;

    void OnDrawGizmos()
    {
        var gizmoColor = Color.green;
        Gizmos.color = gizmoColor;
        if (PointList != null)
        {
            for (int i = 0; i < PointList.Count; i++)
            {
                Gizmos.DrawSphere(transform.position+PointList[i], displayRadius);
            }
        }
    }
}
