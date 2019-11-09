using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Unity.TetrahedralGrowth;
using ZH.Tensegrity;
using System.Linq;

public class Tetrapoint : MonoBehaviour
{
    [SerializeField] private GrowthManager _growthManager;
    [SerializeField] private RigidElement _rigidElement; //bar
    [SerializeField] private NodePoint _vertexpref; 
    List<Vector3> allPoint = new List<Vector3>();
    List<Vector3> cleanPoint = new List<Vector3>();
    List<NodePoint> allNodePoint = new List<NodePoint>();
    public void GetInstantPoint()
    {
        var tetrahedrons = _growthManager.GetTetrahedron();
        var verts = _growthManager.GetMesh().Vertices;

        foreach (var tetra in tetrahedrons)
        {
            var p0 = (Vector3)verts[tetra.Vertex0].Position;
            var p1 = (Vector3)verts[tetra.Vertex1].Position;
            var p2 = (Vector3)verts[tetra.Vertex2].Position;
            var p3 = (Vector3)verts[tetra.Vertex3].Position;
            allPoint.Add(p0);
            allPoint.Add(p1);
            allPoint.Add(p2);
            allPoint.Add(p3);
        }

    }

    public List<Vector3> DeletRepetivePoint(List<Vector3> jointList)
    {
       
        List<Vector3> distinct = jointList.Distinct().ToList();

        return distinct;
    }

    public void InstantiateJoint(List<Vector3> jointList)
    {
        
        foreach (var point in jointList)
        {
            var p = Instantiate(_vertexpref, point,Quaternion.identity);
            allNodePoint.Add(p);
        }
    }

    public void GenerateBar(List<Vector3> jointList)
    {
        foreach (var point in jointList)
        {
            //Instantiate(_rigidElement, point, Quaternion.identity);
        }
    }

    public List<Vector3> GetVertexList()
    {
        return cleanPoint;
    }


    public List<NodePoint> GetNodeList()
    {
        return allNodePoint;
    }

    void Start()
    {

    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetInstantPoint();
            cleanPoint = DeletRepetivePoint(allPoint);
            InstantiateJoint(cleanPoint);
            print(cleanPoint.Count);
        }

    }
}
