using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class FinalPathGenerator : MonoBehaviour
{
    [SerializeField] List<FinalPlatform> platforms;
    [SerializeField] List<Transform> finalPath;
    [SerializeField] FinalPlatform prefabFinalPlatform;
    [SerializeField] CollectableMale prefabCollectableMale;
    int test = 0;

    public Vector3[] PathPoints => finalPath.Select(o => (o.position +Vector3.up * 0.3f)).ToArray();
    private float nodeDistance = 2.2f;
    private Transform pencil;

    private void OnDrawGizmos()
    {
        var col = Gizmos.color;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < finalPath.Count; i++)
        {
            var currentNode = finalPath[i];
            var pos = currentNode.position;
            Gizmos.DrawSphere(pos, 0.1f);
            if (i < finalPath.Count - 1)
            {
                var nextNode = finalPath[i + 1];
                var nextPos = nextNode.position;
                Gizmos.DrawLine(pos, nextPos);
            }
        }

        Gizmos.color = col;
    }
    private void Awake()
    {
        GenerateFromToPath();
    }

    public void GeneratePath(Transform from, Transform to)
    {
        var dir = (to.position - from.position);
        var sqrDistance = dir.sqrMagnitude;

        var nodeCount = ((int)dir.magnitude / (int)(nodeDistance));
        Debug.Log(nodeCount);
        var node = CreateNode();

        for (int i = 0; i < nodeCount; i++)
        {
           
            var obj = Instantiate(prefabCollectableMale);     
            node = CreateNode();
            obj.transform.position = node.position + (dir.normalized*(nodeDistance/2)) + Vector3.up*0.3f;
            //obj.transform.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
            finalPath.Add(node);
            pencil.position += dir.normalized * nodeDistance;
            pencil.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
        }
    }

    [Button]
    private void GenerateFromToPath()
    {
        pencil = new GameObject("Pencil").transform;
        pencil.SetParent(transform);
        pencil.position = platforms[0].TopPoint.position;
        pencil.rotation = platforms[0].TopPoint.rotation;
        for(int i = 0; i < platforms.Count-1; i++)
        {
            var from = platforms[i].TopPoint;
            var to = platforms[i+1].BottomPoint;
            GeneratePath(from, to);
            pencil.position = platforms[i+1].TopPoint.position;
            pencil.rotation = platforms[i+1].TopPoint.rotation;
        }
        
    }

    [Button]
    private void GeneratePlatform()
    {
        var index = platforms.Count == 0 ? 0 : platforms.Count;
        var offset = (nodeDistance*test)*Vector3.forward;
        test++;
        var position = index * (Vector3.forward * (8f));
        Debug.Log(offset + position);
        var obj = Instantiate(prefabFinalPlatform, position, Quaternion.identity);
        platforms.Add(obj);

    }

    private Transform CreateNode()
    {
        var node = new GameObject().transform;
        node.SetParent(transform);
        node.position = pencil.position;
        node.rotation = pencil.rotation;
        Debug.Log($"point created {pencil.position}");

        return node;
    }
}
