using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public List<Transform> PointList;
    public List<bool> IsTaken;
    [SerializeField] private float displayRadius = 1;

    void OnDrawGizmos()
    {
        var gizmoColor = Color.green;
        Gizmos.color = gizmoColor;
        if (PointList != null)
        {
            Vector3 offset;
            for (int i = 0; i < PointList.Count; i++)
            {
                offset = PointList[i].position - transform.position;
                Gizmos.DrawSphere(transform.position+ offset, displayRadius);
            }
        }
    }
}
