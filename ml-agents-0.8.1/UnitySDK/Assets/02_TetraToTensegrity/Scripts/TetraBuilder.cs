using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Unity.TetrahedralGrowth;
using ZH.Tensegrity;
using SpatialSlur.Mesh;
using SpatialSlur.Mesh.Impl;
using SpatialSlur.Core;
using UnityEngine.UI;



public class TetraBuilder : MonoBehaviour
{

    [SerializeField] GrowthManager _manager;
    List<Tetra> tetrahedrons;
    NodeList<HeMesh3d.Vertex> vertices;

    [SerializeField] private Material material;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            BuildMesh();
        }
       
    }

   

    public void BuildTetra()
    {
        tetrahedrons = _manager.GetTetrahedron();
        vertices = _manager.GetMesh().Vertices;

        print("BuildTetraBuildTetraBuildTetraBuildTetra" + tetrahedrons.Count);

        foreach (var tetra in tetrahedrons)
        {
            Vector3 v0 = (Vector3)vertices[tetra.Vertex0].Position;
            Vector3 v1 = (Vector3)vertices[tetra.Vertex1].Position;
            Vector3 v2 = (Vector3)vertices[tetra.Vertex2].Position;
            Vector3 v3 = (Vector3)vertices[tetra.Vertex3].Position;

            v0 = transform.TransformPoint(v0);
            v1 = transform.TransformPoint(v1);
            v2 = transform.TransformPoint(v2);
            v3 = transform.TransformPoint(v3);

            CreateMesh(v0, v1, v2, v3);
        }
    }

    void SetPointPrefab(Vector3 point)
    {
        
    }

    public void BuildMesh()
    {
        tetrahedrons = _manager.GetTetrahedron();
        vertices = _manager.GetMesh().Vertices;
        print("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh" + tetrahedrons.Count);


        foreach (var tetra in tetrahedrons)
        {
            Vector3 v0 = (Vector3)vertices[tetra.Vertex0].Position;
            Vector3 v1 = (Vector3)vertices[tetra.Vertex1].Position;
            Vector3 v2 = (Vector3)vertices[tetra.Vertex2].Position;
            Vector3 v3 = (Vector3)vertices[tetra.Vertex3].Position;

            v0 = transform.TransformPoint(v0);
            v1 = transform.TransformPoint(v1);
            v2 = transform.TransformPoint(v2);
            v3 = transform.TransformPoint(v3);

            CreateMesh(v0, v1, v2, v3);
        }
    }


    public void CreateMesh(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3[] verticesMesh = new Vector3[12];
        Vector3[] normals = new Vector3[verticesMesh.Length];
        int i = 0;
        normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v2 - v0, v1 - v0).normalized;
        verticesMesh[i++] = v0;
        verticesMesh[i++] = v2;
        verticesMesh[i++] = v1;

        normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v1 - v0, v3 - v0).normalized;
        verticesMesh[i++] = v0;
        verticesMesh[i++] = v1;
        verticesMesh[i++] = v3;

        normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v3 - v0, v2 - v0).normalized;
        verticesMesh[i++] = v0;
        verticesMesh[i++] = v3;
        verticesMesh[i++] = v2;

        normals[i] = normals[i + 1] = normals[i + 2] = Vector3.down;
        verticesMesh[i++] = v1;
        verticesMesh[i++] = v2;
        verticesMesh[i++] = v3;
        int[] triangles = new int[verticesMesh.Length];
        for (int n = 0; n < triangles.Length; n++)
            triangles[n] = n;

        //MeshFilter mf = GetComponent<MeshFilter>();
        //Mesh mesh = mf.mesh;
        //mesh.Clear();
        Mesh mesh = new Mesh();
        mesh.vertices = verticesMesh;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateBounds();
        GameObject tetraGameObject = new GameObject("TetraMesh", typeof(MeshFilter), typeof(MeshRenderer));
        tetraGameObject.tag = "TetraMesh";
        tetraGameObject.GetComponent<MeshFilter>().mesh = mesh;
        material.color=new Color(1,1,1,0.2f);
        tetraGameObject.GetComponent<MeshRenderer>().material = material;
    }

}
