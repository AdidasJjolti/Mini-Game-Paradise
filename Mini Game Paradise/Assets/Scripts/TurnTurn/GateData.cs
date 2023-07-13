using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GateData
{
    static List<float> _verticalDistance = new List<float> { 0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f};
    static List<(float, float)> _horizontalDistance = new List<(float, float)> { (0.1f, 0.2f), (0.2f, 0.4f), (0.2f, 0.6f), (0.2f, 0.8f), (0.2f, 1.0f), (0.2f, 1.2f), (0.2f, 1.4f), (0.2f, 1.6f), (0.2f, 1.8f), (0.2f, 2.0f) };

    public static float SetVerticalDistance(int count)
    {
        return _verticalDistance[Mathf.Min(count / 2, _verticalDistance.Count - 1)];
    }

    public static (float, float) SetHorizontalDistance(int count)
    {
        return _horizontalDistance[Mathf.Min(count / 2, _horizontalDistance.Count - 1)];
    }

    public static float SetTreeGap()
    {
        int gap = 0;
        return gap;
    }
}
