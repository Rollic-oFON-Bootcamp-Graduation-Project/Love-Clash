using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriangularNumber
{
    /// <summary>
    /// Checks if a number is a triangular or not.
    /// </summary>
    /// <param name="n">Number to be checked.</param>
    /// <returns>Returns true if number is triangular returns false if not.</returns>
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
    /// <summary>
    /// Closest triangular number to n.
    /// </summary>
    /// <param name="n">Number n</param>
    /// <returns>Returns closest triangular number to n.</returns>
    public static int ClosestRoot(int n)
    {
        return Mathf.FloorToInt((Mathf.Sqrt((8 * n + 1)) - 1) / 2);
    }
    /// <summary>
    /// To find a triangle number at n.
    /// </summary>
    /// <param name="n">Number n</param>
    /// <returns>Returns a triangle number at n</returns>
    public static int TriangleNumber(int n)
    {
        return (n * (n + 1)) / 2;
    }
}
