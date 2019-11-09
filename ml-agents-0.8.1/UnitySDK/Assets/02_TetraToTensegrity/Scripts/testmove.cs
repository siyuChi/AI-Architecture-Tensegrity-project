using System;
using System.Collections;
using System.Collections.Generic;
using MLAgents;
using SpatialSlur;
using UnityEngine;
using UnityEngine.UI;
using ZH.Tensegrity;
using Random = UnityEngine.Random;
using System.Linq;

public class testmove : MonoBehaviour
{

    [SerializeField] private NodePoint intelPoint;

    [SerializeField] GameObject allPointObj;

    [SerializeField] GameObject _target;
    List<Vector3> allPosition = new List<Vector3>();
    List<Vector3> deltaPosition = new List<Vector3>();
    private float moveSpeed = 2f;
    private float noin_moveSpeed = 0.5f;
    private Vector3 previousip;
    private List<Vector3> originaldeltaPosition = new List<Vector3>(); //原始位置无作用力
    private Vector3 ip;
    private Vector3 tp;
    private NodePoint intelPointNb;
    private NodePoint[] nointelPointNb;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        nointelPointNb = allPointObj.GetComponentsInChildren<NodePoint>();

        foreach (var n in nointelPointNb)
        {
            n.position = n.transform.localPosition;
            allPosition.Add(n.position);
            //print(n.position);
        }

        ip = intelPoint.transform.localPosition;
        tp = _target.GetComponent<Rigidbody>().position;
        dir = tp - ip;
        intelPointNb = intelPoint.GetComponent<NodePoint>();
        originaldeltaPosition = deltaPosition;
    }

    // Update is called once per frame
    void Update()
    {

        tp = _target.GetComponent<Rigidbody>().position;
        ip = intelPoint.position;
        //print("ip is" + ip);
        
        //youwenti
        for (int i = 0; i < allPosition.Count; i++)
        {
             
            deltaPosition.Add(ip - allPosition[i]);
            // print(deltaPosition[i]);
        }

        dir = tp - ip;

        // add force for intelligent part
        intelPointNb.GetComponent<Rigidbody>().AddForce(dir * moveSpeed , ForceMode.Force);

        if (intelPointNb.GetComponent<Rigidbody>().velocity.sqrMagnitude > 25f) // slow it down
        {
            intelPointNb.GetComponent<Rigidbody>().velocity = intelPointNb.GetComponent<Rigidbody>().velocity*0.6f ;
        }


        //add linked force for non - intelligent part
        //for (int i = 0; i < allPosition.Count; i++)
        //{
        //    if (deltaPosition[i].magnitude > originaldeltaPosition[i].magnitude) //p1-p0>0
        //    {
        //        var dirr = deltaPosition[i].normalized;
        //        var deltap = deltaPosition[i] - originaldeltaPosition[i]; //deltap = p1-p0
        //        nointelPointNb[i].GetComponent<Rigidbody>().AddForce((noin_moveSpeed * deltap.magnitude) * dirr, ForceMode.Force);
        //    }
        //    else
        //    {
        //        nointelPointNb[i].GetComponent<Rigidbody>().velocity = 0.1f * nointelPointNb[i].GetComponent<Rigidbody>().velocity;
        //        print("stay"); //速度减慢趋于静止
        //    }

        //}

        //store previousposition information
        previousip = ip;
    }
}
