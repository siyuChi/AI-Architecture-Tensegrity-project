using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class AgentMethod : MonoBehaviour
{

    public float FindCenter(float _height, List<Vector3> Pos, List<TensileElement> _String)
    {
        Pos.Clear();

        foreach (var a in _String)
        {
            var EP = a.EndPt;
            var SP = a.StartPt;
            Vector3 Epos = EP.position;
            Vector3 Spos = SP.position;

            if (!Pos.Contains(Epos))
            {
                Pos.Add(Epos);
            }

            if (!Pos.Contains(Spos))
            {
                Pos.Add(Spos);
            }
        }

        Pos.Distinct();

        Vector3 sum = Vector3.zero;

        foreach (var p in Pos)
        {
            sum += p;
        }
        var averagePos = sum / Pos.Count;
        _height = averagePos.y;
        return _height;

    }

    public float SetCenter(float _height, List<Vector3> Pos, List<TensileElement> _String)
    {
        Pos.Clear();

        foreach (var a in _String)
        {
            var EP = a.EndPt;
            var SP = a.StartPt;
            Vector3 Epos = EP.position;
            Vector3 Spos = SP.position;

            if (!Pos.Contains(Epos))
            {
                Pos.Add(Epos);
            }

            if (!Pos.Contains(Spos))
            {
                Pos.Add(Spos);
            }
        }

        Pos.Distinct();

        Vector3 sum = Vector3.zero;

        foreach (var p in Pos)
        {
            sum += p;
        }
        var averagePos = sum / Pos.Count;
        _height = averagePos.y;
        return _height;

    }
    void SetUnitAsDefualt(TensegrityObject abObj)
    {
        abObj.SetAsDefualt();
    }
    private void AddJoint(List<TensileElement> _String, List<NodePoint> Joints)
    {

        foreach (var a in _String)
        {
            var EP = a.EndPt;
            var SP = a.StartPt;

            if (!Joints.Contains(SP))
            {
                Joints.Add(SP);
            }
            if (!Joints.Contains(EP))
            {
                Joints.Add(EP);
            }
        }
        Joints.Distinct();
    }
    private float GetLength(float action,float minLength,float maxLength)
    {
        float a = Mathf.Clamp(action, -1f, 1f);

        float t = (a + 1f) * 0.5f;

        return Mathf.Lerp(minLength, maxLength, t);

    }
    private float GetParameter(float action)
    {
        float value = 2f;
        float a = Mathf.Clamp(action, -1f, 1f);
        a = a * value;
        return a;

    }


    private void ChangeColor()
    {
        //if (agentType == AgentType.StringA)
        //{
        //    foreach (var a in _String)
        //    {
        //        a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        //    }
        //}
        //if (agentType == AgentType.StringB)
        //{
        //    foreach (var a in _String)
        //    {
        //        a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        //    }
        //}
        //if (agentType == AgentType.COString)
        //{
        //    foreach (var a in _String)
        //    {
        //        a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        //    }
        //}

    }
}
