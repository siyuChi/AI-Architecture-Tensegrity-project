using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Unity.TetrahedralGrowth;
using ZH.Tensegrity;
using ZH.Tensegrity.Support;


public class TwoTypesBuilder : MonoBehaviour
{
    [SerializeField] GrowthManager _manager;
    [SerializeField] TetrahedronTypeA _typeA;
    [SerializeField] TetrahedronTypeB _typeB;
    [SerializeField] PrefabContainer prefabs;

    List<TetrahedronTypeA> structuresA;
    List<TetrahedronTypeB> structuresB;

    bool isFreezing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GrowTensegrityWithTwoType();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Move(structuresA,structuresB);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
         BuildConnection();
        }
    }

    public TetrahedronTypeA BuildTensegrityTypeA(Tetra tetra, SpatialSlur.Mesh.Impl.NodeList<SpatialSlur.Mesh.HeMesh3d.Vertex> verts)
    {
       
        var p0 = (Vector3)verts[tetra.Vertex0].Position;
        var p1 = (Vector3)verts[tetra.Vertex1].Position;
        var p2 = (Vector3)verts[tetra.Vertex2].Position;
        var p3 = (Vector3)verts[tetra.Vertex3].Position;

        p0 = transform.TransformPoint(p0);
        p1 = transform.TransformPoint(p1);
        p2 = transform.TransformPoint(p2);
        p3 = transform.TransformPoint(p3);


        var cen = (p0 + p1 + p2 + p3) / 4f;
        var tetratype = Instantiate(_typeA, transform);
        tetratype.transform.position = cen;

        p0 = tetratype.transform.InverseTransformPoint(p0);
        p1 = tetratype.transform.InverseTransformPoint(p1);
        p2 = tetratype.transform.InverseTransformPoint(p2);
        p3 = tetratype.transform.InverseTransformPoint(p3);


        tetratype.SetupTetraPoints(new Vector3[] { p0, p1, p2, p3 });
        tetratype.SetupStructure();
       
        return tetratype;
    }
    public TetrahedronTypeB BuildTensegrityTypeB(Tetra tetra, SpatialSlur.Mesh.Impl.NodeList<SpatialSlur.Mesh.HeMesh3d.Vertex> verts)
    {
       
        var p0 = (Vector3)verts[tetra.Vertex0].Position;
        var p1 = (Vector3)verts[tetra.Vertex1].Position;
        var p2 = (Vector3)verts[tetra.Vertex2].Position;
        var p3 = (Vector3)verts[tetra.Vertex3].Position;

        p0 = transform.TransformPoint(p0);
        p1 = transform.TransformPoint(p1);
        p2 = transform.TransformPoint(p2);
        p3 = transform.TransformPoint(p3);


        var cen = (p0 + p1 + p2 + p3) / 4f;
        var tetratype = Instantiate(_typeB, transform);
        tetratype.transform.position = cen;

        p0 = tetratype.transform.InverseTransformPoint(p0);
        p1 = tetratype.transform.InverseTransformPoint(p1);
        p2 = tetratype.transform.InverseTransformPoint(p2);
        p3 = tetratype.transform.InverseTransformPoint(p3);


        tetratype.SetupTetraPoints(new Vector3[] { p0, p1, p2, p3 });
        tetratype.SetupStructure();
        return tetratype;
        
    }
    public void GrowTensegrityWithTwoType()
    {
        if (structuresA == null)
        {
            structuresA = new List<TetrahedronTypeA>();
        }
        if (structuresB == null)
        {
            structuresB = new List<TetrahedronTypeB>();
        }
     

        var tetrahedrons = _manager.GetTetrahedron();
        var verts = _manager.GetMesh().Vertices;


        if (tetrahedrons.Count == structuresA.Count+structuresB.Count)
        {
            return;
        }

        int index = 0;
        for (int i = 0; i < tetrahedrons.Count; i++)
        {
     
            var tetra = tetrahedrons[i];

            if (i%2==0)
            {
              var tetratype = BuildTensegrityTypeA(tetra,verts);
                tetratype.name = "tetra_A_" + i;
                structuresA.Add(tetratype);
            }
            else
             {
               var tetratype= BuildTensegrityTypeB(tetra,verts);
                 structuresB.Add(tetratype);
                tetratype.name = "tetra_B_" + i;
            }

            index++;
        }
        

    }
    public void Move(List<TetrahedronTypeA> _tA, List<TetrahedronTypeB> _tB )
    {
        foreach (var a in _tA)
        {
            a.transform.localPosition+=new Vector3(0,20f,0);
        }
        foreach (var b in _tB)
        {
            b.transform.localPosition += new Vector3(0, 20f, 0);
        }
    }
  
    public void BuildConnection()
    {
        for (int i = 0; i < structuresA.Count&&i<structuresB.Count; i++)
        {
            var connection = Instantiate(prefabs.m_BarPrefab, transform);

            connection.SetUp(structuresA[i].transform.localPosition, structuresB[i].transform.localPosition, 0.05f);

          
        }
    }

}
