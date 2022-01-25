using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{
    [SerializeField] private Transform upperLimit, bottomLimit;
    [SerializeField] private List<Collectable> collectables;
    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        var gizmoColor = Color.green;
        gizmoColor.a = 0.4f;
        Gizmos.color = gizmoColor;

        var center = Vector3.Lerp(upperLimit.position, bottomLimit.position, 0.5f);
        center.y = 0.6f;
        var size = upperLimit.position - bottomLimit.position;
        size.y = 1f;

        Gizmos.DrawCube(center, size);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Observer.StackHandleBattle?.Invoke(upperLimit.position, bottomLimit.position, collectables);
        }
    }
}
