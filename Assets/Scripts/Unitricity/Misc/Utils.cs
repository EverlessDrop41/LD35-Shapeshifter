using UnityEngine;
using System;
using System.Collections;

public class Utils
{
    public static bool RoughlyEqual(float a, float b)
    {
        return RoughlyEqual(a, b, 1);
    }

    public static bool RoughlyEqual (float a, float b, float threshold)
    {
        return (Math.Abs(a - b) < threshold);
    }

    public static bool VectorRoughlyEqual(Vector3 a, Vector3 b)
    {
        return VectorRoughlyEqual(a, b, 1);
    }

    public static bool VectorRoughlyEqual (Vector3 a, Vector3 b, float threashold)
    {
        return (RoughlyEqual(a.x, b.x, threashold) && 
            RoughlyEqual(a.y, b.y, threashold) && 
            RoughlyEqual(a.z, b.z, threashold));
    }
}
