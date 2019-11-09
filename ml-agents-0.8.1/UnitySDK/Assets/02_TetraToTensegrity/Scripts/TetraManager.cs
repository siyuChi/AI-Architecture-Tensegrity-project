using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Unity.TetrahedralGrowth;
using ZH.Tensegrity;
using SpatialSlur.Mesh;
using SpatialSlur.Mesh.Impl;
using SpatialSlur.Core;
using UnityEngine.UI;

public class TetraManager : MonoBehaviour
{
    [SerializeField] GrowthManager _manager;
    List<Tetra> tetrahedrons;
    NodeList<HeMesh3d.Vertex> vertices;

    [SerializeField] private GameObject Target;
    private Rigidbody Trb;
    [SerializeField] private float _period = 3.0f; // Time in seconds per cycle


    private float _distance;
    [SerializeField] Vector3 _MoveDistance=new Vector3(0,0,0);

    public void ChaseMovement()
    {

        foreach (var tetra in _manager.GetTetrahedron())
        {
            Vector3 origin = (Vector3)_manager.GetMesh().Vertices[tetra.Vertex0].Position;

            _distance = _MoveDistance.magnitude*0.1f;

            //next
            _manager.GetMesh().Vertices[tetra.Vertex1].Position =
                (_manager.GetMesh().Vertices[tetra.Vertex0].Position - _manager.GetMesh().Vertices[tetra.Vertex1].Position) * _distance +
                _manager.GetMesh().Vertices[tetra.Vertex1].Position;

            _manager.GetMesh().Vertices[tetra.Vertex2].Position =
                (_manager.GetMesh().Vertices[tetra.Vertex0].Position - _manager.GetMesh().Vertices[tetra.Vertex2].Position) * _distance +
                _manager.GetMesh().Vertices[tetra.Vertex2].Position;

            _manager.GetMesh().Vertices[tetra.Vertex3].Position =
                (_manager.GetMesh().Vertices[tetra.Vertex0].Position - _manager.GetMesh().Vertices[tetra.Vertex3].Position) * _distance +
                _manager.GetMesh().Vertices[tetra.Vertex3].Position;
        }
    }

     void MoveTarget()
    {
      
        Trb.AddForce(Random.onUnitSphere,ForceMode.Force);


    }

    public void MovePoints(Vector3 poss)
    {
        var dir = (Target.transform.position - poss).normalized;//dir to target
        
        float t = Time.deltaTime * _period;
        //poss = Vector3.Lerp(poss, poss + _MoveDistance, t);
        poss += dir;
    }

    void Awake()
    {
       Trb = Target.AddComponent<Rigidbody>();
        Trb.useGravity = false;
    }
    // Update is called once per frame
    void Update()
    {
        MoveTarget();


        if (Input.GetKey(KeyCode.A))
        {
            
            MovePoints((Vector3)_manager.GetMesh().Vertices[_manager.GetTetrahedron()[15].Vertex0].Position);

            ChaseMovement();

        }

    }


}
