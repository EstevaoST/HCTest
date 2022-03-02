using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 Forward2D(this Transform t)
    {
        return t.right;
    }

    public static T[] Populate<T>(T value, int count)
    {
        T[] arr = new T[count];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = value;
        }
        return arr;
    }

    public static void SetLayerRecursively(this GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}