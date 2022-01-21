using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoadManager : MonoBehaviour

{
    [BoxGroup("Objects")]
    [SerializeField] private GameObject myRoad;
    [BoxGroup("Parent")]
    [SerializeField] private Transform roadParent;
    [BoxGroup("Created Objects List")]
    [SerializeField] private List<GameObject> roads;

    [BoxGroup("Road Settings")]
    public float roadLength = 1f;
    [BoxGroup("Road Settings")]
    public float roadWidth = 1f;
    [BoxGroup("Road Settings")]
    public int roadCount;

    private static RoadManager instance;
    public static RoadManager Instance => instance ?? (instance = instance = FindObjectOfType<RoadManager>());
    private void Awake()
    {
        instance = instance ??= this;
        Debug.Log(instance);
    }

    [Button("Create")]
    private void CreateRoad()
    {
        var index = roads.Count == 0 ? 0 : roads.Count;
        var roadPosition = index * (Vector3.forward * roadLength);
        var roadToCreate = myRoad;
        roadToCreate.transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        var newRoad = Instantiate(roadToCreate, roadPosition, Quaternion.identity, roadParent);
        roads.Add(newRoad);
    }

    [Button("Update Road")]
    void UpdateRoad()
    {
        roads[0].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        for (int i = 1;i< roads.Count; i++)
        {
            roads[i].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
            roads[i].transform.position = roads[i - 1].transform.position + (Vector3.forward * roadLength);
        }
    }

    [Button("Delete")]
    void DeleteRoad()
    {
        if (roads.Count == 0) return;
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);
    }

    [Button("Delete All")]
    void DeleteAll()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            DestroyImmediate(roads[i]);
        }
        roads.Clear();
    }

    [Button("Create Multiple")]
    void CreateMultipleRoad()
    {
        for (int i = 0; i < roadCount; i++)
        {
            CreateRoad();
        }
    }

}