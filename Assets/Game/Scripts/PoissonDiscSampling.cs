using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling 
{
    public static List<Vector3> GeneratePoints(float radius, Vector3 regionSize, out int pointCount, Vector3? offset = null, float circleRadius = 0f, int k = 30)
    {
        if(offset == null)
        {
            offset = Vector3.zero;
        }
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(regionSize.x / cellSize), Mathf.CeilToInt(regionSize.z / cellSize)];
       

        List<Vector3> points = new List<Vector3>();
        List<Vector3> spawnPoints = new List<Vector3>();

        //Select the initial sample
        //Center of the region size
        spawnPoints.Add(regionSize / 2);

        while(spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            var spawnCentre = spawnPoints[spawnIndex];
            bool isAccepted = false;

            for (int i = 0; i < k; i++)
            {
                var angle = Random.value * Mathf.PI * 2;
                var dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                var point = spawnCentre + dir * Random.Range(radius, 2 * radius);
                if (IsValid(point, regionSize, cellSize, radius, points, grid, circleRadius))
                {
                    points.Add(point);
                    spawnPoints.Add(point);
                    grid[(int)(point.x / cellSize), (int)(point.z / cellSize)] = points.Count;
                    isAccepted = true;
                    break;
                }
            }
            if (!isAccepted) spawnPoints.RemoveAt(spawnIndex);
        }

        //Debug.Log($"{grid.GetLength(0)}, {grid.GetLength(1)} and total points {points.Count} CellSize {cellSize}");
        pointCount = points.Count;
        for(int i =0; i< pointCount; i++)
        {
            points[i] += (Vector3)offset;
        }
        return points;
    }
    static bool IsValid(Vector3 point, Vector3 regionSize, float cellSize, float radius, List<Vector3> points, int[,] grid, float circleRadius = 0f)
    {
        //var checkBounds = circleRadius != 0 
        //    ? (point.x >= 0 && point.x < circleRadius && point.z >= 0 && point.z < circleRadius)
        //    : (point.x >= 0 && point.x < regionSize.x && point.z >= 0 && point.z < regionSize.z);
        if (!(point.x >= 0 && point.x < regionSize.x && point.z >= 0 && point.z < regionSize.z))
        {
            return false;
        }
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {

                int pointIndex = grid[i, j] - 1;
                if (pointIndex != -1)
                {
                    float sqrDst = (point - points[pointIndex]).sqrMagnitude;
                    if (sqrDst < radius * radius)
                    {
                        return false;
                    }
                }
            } 
        }
        if (circleRadius != 0)
        {
            var circleSqrDst = (point - (regionSize / 2)).sqrMagnitude;
            if (circleSqrDst > circleRadius * circleRadius)
            {
                return false;
            }
        }

        return true;
    }
}
