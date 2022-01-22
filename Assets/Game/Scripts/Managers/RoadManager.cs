using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoadManager : MonoBehaviour

{
    [BoxGroup("Objects")]
    [SerializeField] private GameObject myRoad;
    [BoxGroup("Parent")]
    [SerializeField] private Transform roadParent;
    [BoxGroup("Created Objects List")]
    [SerializeField] private List<GameObject> roads;

    [BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadFormat))]
    public int roadLength = 1;
    [BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadFormat))]
    public int roadWidth = 1;
    [BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadCount)), Range(0, 999)]
    public int roadCount;
    private int prevRoadCount;

    private static RoadManager instance;
    public static RoadManager Instance => instance ?? (instance = instance = FindObjectOfType<RoadManager>());
    private void Awake()
    {
        instance = instance ??= this;
        Debug.Log(instance);
    }

#if UNITY_EDITOR
    private void CreateRoad()
    {
        var index = roads.Count == 0 ? 0 : roads.Count;
        var roadPosition = index * (Vector3.forward * roadLength);
        var roadToCreate = myRoad;
        roadToCreate.transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        var newRoad = PrefabUtility.InstantiatePrefab(roadToCreate, roadParent) as GameObject;
        newRoad.transform.position = roadPosition;
        roads.Add(newRoad);
    }
    void UpdateRoadFormat()
    {
        roads[0].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        for (int i = 1;i< roads.Count; i++)
        {
            roads[i].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
            roads[i].transform.position = roads[i - 1].transform.position + (Vector3.forward * roadLength);
        }
    }
    void DeleteRoad()
    {
        if (roads.Count == 0) return;
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);
    }
    void DeleteAll()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            DestroyImmediate(roads[i]);
        }
        roads.Clear();
    }
    void UpdateRoadCount()
    {
        var difference = (roadCount - 1) - roads.Count;
        if(difference > 0)
        {
            for(int i = 0; i < difference; i++)
            {
                CreateRoad();
            }
        }
        else if(difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                DeleteRoad();
            }
        }
    }
#endif
}