using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushup : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(Vector3.up*500f,ForceMode.Acceleration);
    }
}
