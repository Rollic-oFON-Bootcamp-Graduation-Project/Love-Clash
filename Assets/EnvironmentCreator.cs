using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnvironmentCreator : MonoBehaviour
{
    //TODO
    [SerializeField] private List<GameObject> envObjects;
    [SerializeField] private GameObject myQuad;
    [SerializeField] private List<Vector3> envPoints;
    [SerializeField, Range(5, 100)] public float radius = 1;
    [SerializeField, ReadOnly] private int pointCount;
    public float displayRadius = 1;
    private float envSizeX = 100;
    private Vector3 envSize => new Vector3(envSizeX, 1, RoadManager.Instance.RoadCount * 20f);
    private float roadWidth = 8;
    private float roadBound => (roadWidth / 2) + 2;
    private void Start()
    {
        //envSize = new Vector3(200, 1, RoadManager.Instance.RoadCount * 20f);
        myQuad.transform.localScale = 5*envSize;
        Debug.Log(RoadManager.Instance.RoadCount);
        var offset = myQuad.transform.position;
        offset.x = -envSize.x / 2;
        envPoints = PoissonDiscSampling.GeneratePoints(radius, envSize, out pointCount,offset);

        CreateCloudsInPositions();
    }

    private void CreateCloudsInPositions()
    {
        //TODO
        for(int i= 0;i< envPoints.Count; i++)
        {
            Debug.Log(-roadWidth/2);
            if((envPoints[i].x >= -roadBound && envPoints[i].x <= roadBound))
            {
                continue;
            }   
            var objectToSpawnIndex = (int)Random.Range(0, envObjects.Count);
            var randomSize = Random.Range(3, 5);

            var spawnedObject = Instantiate(envObjects[objectToSpawnIndex], envPoints[i], Quaternion.identity, transform);
            spawnedObject.transform.localScale = Vector3.one * randomSize;
        }
    }
    //TODO
  
}
