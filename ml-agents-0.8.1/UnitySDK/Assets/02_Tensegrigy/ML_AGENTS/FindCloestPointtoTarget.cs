using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZH.Tensegrity;

public class FindCloestPointtoTarget : MonoBehaviour
{
    [SerializeField] private TensegrityObject tobj;
    [SerializeField] private ElevenWalkAgent eAgent;
    private Collider[] _colliders;
    private float distance;
    public Vector3 closestPoint;

    void Start()
    {
        
    }

    public void SetTobj(TensegrityObject _obj)
    {
        tobj = _obj;
    }

    /// <summary>
    /// caculate distance between tensegrityobj and targetframe
    /// </summary>
    public float CaculateDistanceinColliders(TensegrityObject _obj)
    {
        _colliders = GetComponentsInChildren<Collider>();

        float dis = float.MaxValue;
        
        foreach (var t in _colliders)
        {
            var cp = t.ClosestPointOnBounds(_obj.elements.center.transform.position);
            var d = Vector3.Distance(cp, _obj.elements.center.transform.position);
            if (d < dis)
            {
                dis = d;
                closestPoint = cp;
            }

        }

        return dis;
    }
    public float CaculateDistance(TensegrityObject _obj)
    {
        var _collider = GetComponentInChildren<Collider>();

        float dis = float.MaxValue;


            var cp = _collider.ClosestPointOnBounds(_obj.elements.center.transform.position);
            var d = Vector3.Distance(cp, _obj.elements.center.transform.position);
            if (d < dis)
            {
                dis = d;
                closestPoint = cp;
            }

       

        return dis;
    }
    void Update()
    {
        if (tobj != null)
        {
            distance = CaculateDistanceinColliders(tobj);
        }
    }

    public Vector3[] FindLine()
    {
        Vector3[] _line = new Vector3[2];
        if (tobj != null)
        {
            _line[0] = tobj.elements.center.transform.position;
            _line[1] = closestPoint;

        }
        return _line;
    }

}

/*
 * 
public class FindCloestPointtoTarget : MonoBehaviour
{
    [SerializeField] private TensegrityObject tobj;
    [SerializeField] private ElevenWalkAgent eAgent;
    private Collider[] _colliders;
    private float distance;
    public Vector3 closestPoint;

    void Start()
    {
        
    }

    public void SetTobj(TensegrityObject _obj)
    {
        tobj = _obj;
    }

    /// <summary>
    /// caculate distance between tensegrityobj and targetframe
    /// </summary>
    public float CaculateDistanceinColliders(TensegrityObject _obj)
    {
        _colliders = GetComponentsInChildren<Collider>();

        float dis = float.MaxValue;
        
        foreach (var t in _colliders)
        {
            var cp = t.ClosestPointOnBounds(_obj.elements.center.transform.position);
            var d = Vector3.Distance(cp, _obj.elements.center.transform.position);
            if (d < dis)
            {
                dis = d;
                closestPoint = cp;
            }

        }

        return dis;
    }
    public float CaculateDistance(TensegrityObject _obj)
    {
        var _collider = GetComponentInChildren<Collider>();

        float dis = float.MaxValue;


            var cp = _collider.ClosestPointOnBounds(_obj.elements.center.transform.position);
            var d = Vector3.Distance(cp, _obj.elements.center.transform.position);
            if (d < dis)
            {
                dis = d;
                closestPoint = cp;
            }

       

        return dis;
    }
    void Update()
    {
        if (tobj != null)
        {
            distance = CaculateDistanceinColliders(tobj);
        }
    }

    public Vector3[] FindLine()
    {
        Vector3[] _line = new Vector3[2];
        if (tobj != null)
        {
            _line[0] = tobj.elements.center.transform.position;
            _line[1] = closestPoint;

        }
        return _line;
    }

}
*/