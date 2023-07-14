using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GateData
{
    static List<float> _verticalDistance = new List<float> { 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f};
    static List<(float, float)> _treeDistance = new List<(float, float)> { (1.0f, 2.0f), (1.0f, 1.9f), (1.0f, 1.8f), (1.0f, 1.7f), (1.0f, 1.6f), (1.0f, 1.55f), (1.0f, 1.5f), (1.0f, 1.45f), (1.0f, 1.4f), (1.0f, 1.3f) };
    static List<(float, float)> _horizontalPosition = new List<(float, float)> { (1.0f, 2.0f), (1.0f, 2.4f), (1.0f, 2.6f), (1.0f, 2.8f), (1.0f, 3.0f), (1.0f, 3.2f), (1.0f, 3.4f), (1.0f, 3.6f), (1.0f, 3.8f), (1.0f, 4.0f) };


    public static float SetVerticalDistance(int count)
    {
        return _verticalDistance[Mathf.Min(count / 2, _verticalDistance.Count - 1)];
    }

    public static (float, float) SetTreeDistance(int count)
    {
        return _treeDistance[Mathf.Min(count / 2, _treeDistance.Count - 1)];
    }

    public static (float, float) SetHorizontalDistance(int count)
    {
        return _horizontalPosition[Mathf.Min(count / 2, _horizontalPosition.Count - 1)];
    }
}
