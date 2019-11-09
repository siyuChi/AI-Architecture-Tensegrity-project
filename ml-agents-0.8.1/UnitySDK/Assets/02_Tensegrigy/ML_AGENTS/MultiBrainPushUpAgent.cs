using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class MultiBrainPushUpAgent : Agent
{
    public AgentType agentType;
    [SerializeField] TensegrityObject abObj;
    [SerializeField] private float height = 0f;
    [SerializeField] private float minLength = 0.6f;
    [SerializeField] private float maxLength = 5f;
    private List<Vector3> Pos = new List<Vector3>();
    [SerializeField] List<TensileElement> _String;
    [SerializeField] MultiBrainPushUpAgent Neighbor1;
    [SerializeField] MultiBrainPushUpAgent Neighbor2;
    private List<NodePoint> Joints = new List<NodePoint>();
    float heightRecord;
    private float prevheight =0f;
    private float length;
    private Vector3 averagePos;

    void SetUnitAsDefualt()
    {
        abObj.SetAsDefualt();
    }

    public  enum AgentType
    {
        StringA,
        StringB,
        COString
    }


    public override void InitializeAgent()
    {
        SetUnitAsDefualt();
        var S = abObj.elements.Strings;
        foreach (var s in S)
        {
            if (s.CompareTag(agentType.ToString()))
            {
                _String.Add(s);
            }
        }

        AddJoint();

        ChangeColor();

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

    private void AddJoint()
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
    public override void CollectObservations()
    {
        // if you want to use the same brain
        //AddVectorObs(agentType == AgentType.StringA);
        //AddVectorObs(agentType == AgentType.StringB);
        //AddVectorObs(agentType == AgentType.COString);
        // if A -> 100, B -> 010, C -> 001
        AddVectorObs(abObj.elements.center.transform.position.y);

            foreach (var s in _String)
            {
                AddVectorObs(s.transform.position);
                AddVectorObs(s.m_ConfigurableJoint.currentForce);
                AddVectorObs(s.m_ConfigurableJoint.currentTorque);
                AddVectorObs(s.length);
            }

        //foreach (var p in abObj.elements.EndPoints)
        //{
        //    AddVectorObs(p.position);
        //}

        foreach (var p in Joints)
        {
            AddVectorObs(p.position);
        }
        //if(agentType == AgentType.COString)
        //if (Neighbor1 != null && Neighbor2 != null)
        {
            float a = 0f;
            a = Neighbor1.FindCenter(a);
            AddVectorObs(a);

            float b = 0f;
            b = Neighbor2.FindCenter(b);
            AddVectorObs(b);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        SetReward(-0.005f);

        


        if (agentType == AgentType.StringA || agentType == AgentType.StringB)
        {

            for (int i = 0; i < vectorAction.Length; i++)
            {
            float length = GetLength(vectorAction[i]);
            _String[i].SetLength(length);
           
            }
        }

        if (agentType == AgentType.COString)//slowly
        {
            for (int i = 0; i < vectorAction.Length; i++)
            {

                //    if (length > minLength && length < maxLength)
                //    {
                //        float deltalength = GetParameter(vectorAction[i]);
                //        _String[i].GetCurlength(length);
                //        _String[i].SetCurLengthByParameter(length, deltalength);
                //    }
                //    else
                //    {
                float length = GetLength(vectorAction[i]);
                    _String[i].SetLength(length);
                //}
            }
        }

        height = 0f;

        height = FindCenter(height);
        float punish = height - heightRecord;

        SetReward(height / 100);

        //AddReward((height + punish) * 0.001f);

        //if (height - prevheight > 5)
        //{
        //    AddReward(-1f);// dont be absolute/proportional
        //}
        //else
        //{
        //    if (agentType == AgentType.StringA)
        //    {

        //        if (height > heightRecord)
        //        {
        //            AddReward(0.1f);// dont be absolute/proportional
        //            heightRecord = height;
        //        }
        //        if (height > 160f)
        //        {
        //            print("good");
        //            SetReward(1f);
        //            Done();
        //        }

        //        if (height > 160f) //40f
        //        {
        //            SetReward(-1f);
        //            Done();
        //        }
        //        if (heightRecord - height > 10f)
        //        {
        //            print("bad");
        //            SetReward(-1f);
        //            Done();
        //        }

        //    }
        //    if (agentType == AgentType.StringB)
        //    {

        //        if (height > heightRecord)
        //        {
        //            AddReward(0.1f);
        //            heightRecord = height;
        //        }
        //        if (height > 200f) //30f
        //        {
        //            print("good");
        //            SetReward(1f);
        //            Done();
        //        }

        //        if (heightRecord - height > 10f)
        //        {
        //            print("bad");
        //            SetReward(-1f);
        //            Done();
        //        }
        //    }
        //    if (agentType == AgentType.COString)
        //    {

        //        if (height > heightRecord)
        //        {
        //            AddReward(0.1f);
        //            heightRecord = height;
        //        }
        //        if (height > 200f)//40
        //        {
        //            print("good");
        //            SetReward(1f);
        //            Done();
        //        }

        //        if (heightRecord - height > 10f)
        //        {
        //            print("bad");
        //            SetReward(-1f);
        //            Done();
        //        }
        //    }
        //}
        //prevheight = height;
        Monitor.Log("Reward", GetReward(), abObj.elements.center.transform);

        if (Neighbor1 != null && Neighbor2 != null)
        {
            //consider the neighbor
            float neighborreward = Neighbor1.GetReward();
            float neighborreward2 = Neighbor2.GetReward();
            AddReward((neighborreward + neighborreward2) * 0.1f);//dont need 0.1
        }
    }

    public float FindCenter(float _height)
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
        averagePos = sum / Pos.Count;
        _height = averagePos.y;
        return _height;

    }
    private float GetLength(float action)
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
    public override void AgentReset()
    {
        if (Neighbor1.IsDone() == true && Neighbor2.IsDone() == true)
        {
            abObj.ResetStructure();
        }
        heightRecord = 0f;
    }

    public override void AgentOnDone()
    {

    }
}
