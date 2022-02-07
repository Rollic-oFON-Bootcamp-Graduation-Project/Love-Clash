using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FinalPathGenerator : MonoBehaviour
{
    [SerializeField] private List<FinalPlatform> platforms;
    [SerializeField] private Transform platformParent, pathParent;
    [SerializeField] private List<Transform> finalPath;
    [SerializeField] private FinalPlatform prefabFinalPlatform;
    [SerializeField] private CollectableMale prefabCollectableMale;
#if UNITY_EDITOR
    [BoxGroup("Road Settings"), OnValueChanged(nameof(UpdatePlatformCount)), Range(0, 10)]
#endif
    public int PlatformCount;
    private int prevPlatformCount;

    public int PathNodeCount => (PlatformCount * (PlatformCount + 1)) / 2;
    public List<Vector3> FinalPath => finalPath.Select(o => (o.position +Vector3.up * 0.05f)).ToList();
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
            Gizmos.DrawSphere(pos, 1f);
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
        //GenerateFromToPath();
    }

    [Button]
    private void GenerateFromToPath()
    {
        pencil = new GameObject("Pencil").transform;
        pencil.SetParent(pathParent);
        pencil.position = platforms[0].TopPoint.position;
        pencil.rotation = platforms[0].TopPoint.rotation;
        for (int i = 0; i < platforms.Count - 1; i++)
        {
            var from = platforms[i].TopPoint;
            var to = platforms[i + 1].BottomPoint;
            GeneratePath(from, to);
            pencil.position = platforms[i + 1].TopPoint.position;
            pencil.rotation = platforms[i + 1].TopPoint.rotation;
        }

        finalPath.Add(pencil);

    }
    private void GeneratePath(Transform from, Transform to)
    {
        var dir = (to.position - from.position);

        var nodeCount = ((int)dir.magnitude / (int)(nodeDistance));
        Debug.Log(nodeCount);
        var node = CreateNode();
        //finalPath.Add(node);

        for (int i = 0; i < nodeCount; i++)
        {
            node = CreateNode();
            node.position += node.forward * (nodeDistance / 2);
            finalPath.Add(node);
            pencil.position += dir.normalized * nodeDistance;
            pencil.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
        }
    }
    private Transform CreateNode()
    {
        var node = new GameObject().transform;
        node.SetParent(pathParent);
        node.position = pencil.position;
        node.rotation = pencil.rotation;

        return node;
    }
#if UNITY_EDITOR
    private void CreatePlatform()
    {
        var index = platforms.Count == 0 ? 0 : platforms.Count;
        var platformPos = transform.position + (Vector3.forward * (8f)) * index;
        var offset = (Vector3.forward * (8f)) + (nodeDistance * index) * Vector3.forward;
        var obj = PrefabUtility.InstantiatePrefab(prefabFinalPlatform, platformParent) as FinalPlatform;
        obj.transform.position = index !=0 ? platforms[index-1].transform.position+offset : platformPos;
        platforms.Add(obj);
    }
    private void DeletePlatform()
    {

        if (platforms.Count == 0) return;
        DestroyImmediate(platforms[platforms.Count - 1].gameObject);
        platforms.RemoveAt(platforms.Count - 1);
    }

    
    private void UpdatePlatformCount()
    {
        var difference = (PlatformCount) - platforms.Count;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                CreatePlatform();
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                DeletePlatform();
            }
        }
    }
#endif
}
