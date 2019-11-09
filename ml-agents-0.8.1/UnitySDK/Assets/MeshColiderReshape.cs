using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColiderReshape : MonoBehaviour
{

  
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = transform.GetComponentInChildren<MeshFilter>().mesh;
        MeshCollider col = this.GetComponent<MeshCollider>();
        col.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
