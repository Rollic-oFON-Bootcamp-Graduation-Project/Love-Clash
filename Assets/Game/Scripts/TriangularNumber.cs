using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriangularNumber
{
    public static bool CheckIfNumberIsTriangular(int n)
    {
        if (n == 0) return true;
        var value = (8 * n) + 1;
        //Debug.Log(value);
        for (int i = 1; i < value / 2; i++)
        {
            if ((value % i == 0) && (value / i == i))
            {
                return true;
            }
        }
        return false;
    }
    public static int ClosestRoot(int n)
    {
        return Mathf.FloorToInt((Mathf.Sqrt((8 * n + 1)) - 1) / 2);
    }
    public static int TriangleNumber(int n)
    {
        return (n * (n + 1)) / 2;
    }
}
