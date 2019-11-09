using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    private List<int> onFacePoints;

    public Face()
    {
        onFacePoints=new List<int>();
    }

    public void AddPoint(int index)
    {
        onFacePoints.Add(index);
    }

    public void AddPoint(int[] indexes)
    {
        onFacePoints.AddRange(indexes);
    }

    public List<int> GetPoints()
    {
        return onFacePoints;
    }
}
