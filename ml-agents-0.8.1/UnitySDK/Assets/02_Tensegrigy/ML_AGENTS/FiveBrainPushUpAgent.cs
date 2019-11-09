using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class FiveBrainPushUpAgent : Agent
{
    public AgentType agentType;
    [SerializeField] TensegrityObject abObj;
    [SerializeField] private float height = 0f;
    [SerializeField] private float minLength = 0.6f;
    [SerializeField] private float maxLength = 5f;
    private List<Vector3> Pos = new List<Vector3>();
    [SerializeField] List<TensileElement> _String;
    [SerializeField] FiveBrainPushUpAgent[] Neighbor = new FiveBrainPushUpAgent[5];
    float neighborreward =0f;
    private List<NodePoint> Joints = new List<NodePoint>();
    float heightRecord;
    private float prevheight =0f;
    private float length;
    void SetUnitAsDefualt()
    {
        abObj.SetAsDefualt();
    }

    public  enum AgentType
    {
        StringA,
        StringB,
        StringC,
        StringD,
        StringE,
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
        //AddNeighbor();
    }
    private void ChangeColor()
    {
        if (agentType == AgentType.StringA)
        {
            foreach (var a in _String)
            {
                a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }
        }
        if (agentType == AgentType.StringB)
        {
            foreach (var a in _String)
            {
                a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }
        }
        if (agentType == AgentType.COString)
        {
            foreach (var a in _String)
            {
                a.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }
        }

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
        if (Neighbor != null) //
        {
            foreach (var n in Neighbor)
            {
                float a = 0f;
                a = n.FindCenter(a);
                AddVectorObs(a);
            }
            
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        SetReward(-0.005f);

        //consider neighbor reward
        if (Neighbor != null)
        {
            foreach (var n in Neighbor)
            {
               neighborreward += n.GetReward();
            }
            AddReward(neighborreward * 0.1f);
        }

        if (agentType == AgentType.StringA || agentType == AgentType.StringB || 
            agentType == AgentType.StringC || agentType == AgentType.StringD || agentType == AgentType.StringE)
        {
            for (int i = 0; i < vectorAction.Length; i++)
            {
            float length = GetLength(vectorAction[i]);
            _String[i].SetLength(length);
            }
        }
        else if (agentType == AgentType.COString)
        {
            for (int i = 0; i < vectorAction.Length; i++)
            {
                float length = GetLength(vectorAction[i]);
                    _String[i].SetLength(length);

            }
        }

        height = 0f;

        height = FindCenter(height);
        float punish = height - heightRecord;

        AddReward((height + punish) * 0.001f);
        if (height - prevheight > 5 )
        {
            AddReward(-1f);
            Done();
        }
        else { 
        if (agentType == AgentType.StringA)
        {
           
            if (height > heightRecord)
            {
                AddReward(0.01f);
                heightRecord = height;
            }
            if (height > 20f&&height <25f)
            {
                //print("good");
                SetReward(1f);
                Done();
            }
            if (height > 30f)
            {
                //print("good");
                SetReward(-0.5f);
                Done();
            }
                if (heightRecord - height > 10f)
            {
                //print("bad");
                SetReward(-1f);
                Done();
            }
            
        }
        if (agentType == AgentType.StringB)
        {
            
            if (height > heightRecord)
            {
                AddReward(0.1f);
                heightRecord = height;
            }
            if (height > 35f) //30f
            {
                //print("good");
                SetReward(1f);
                Done();
            }

            if (heightRecord - height > 10f)
            {
                //print("bad");
                SetReward(-1f);
                Done();
            }
        }
            if (agentType == AgentType.StringC)
            {

                if (height > heightRecord)
                {
                    AddReward(0.1f);
                    heightRecord = height;
                }
                if (height > 50f) //30f
                {
                    //print("good");
                    SetReward(1f);
                    Done();
                }

                if (heightRecord - height > 10f)
                {
                    //print("bad");
                    SetReward(-1f);
                    Done();
                }
            }
            if (agentType == AgentType.StringD)
            {

                if (height > heightRecord)
                {
                    AddReward(0.1f);
                    heightRecord = height;
                }
                if (height > 90f) //30f
                {
                    //print("good");
                    SetReward(1f);
                    Done();
                }

                if (heightRecord - height > 10f)
                {
                    //print("bad");
                    SetReward(-1f);
                    Done();
                }
            }
            if (agentType == AgentType.StringE)
            {

                if (height > heightRecord)
                {
                    AddReward(0.1f);
                    heightRecord = height;
                }
                if (height > 120f) //30f
                {
                    //print("good");
                    SetReward(1f);
                    Done();
                }

                if (heightRecord - height > 10f)
                {
                    //print("bad");
                    SetReward(-1f);
                    Done();
                }
            }
            if (agentType == AgentType.COString)
        {

            if (height > heightRecord)
            {
                AddReward(0.1f);
                heightRecord = height;
            }
            if (height > 40f)//40
            {
                print("good");
                SetReward(1f);
                Done();
            }

            if (heightRecord - height > 10f)
            {
                print("bad");
                SetReward(-1f);
                Done();
            }
            }
        }
        prevheight = height;
        //Monitor.Log("Reward", GetReward(),_String[0].transform);
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
        var averagePos = sum / Pos.Count;
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
        int i = 0;
        foreach (var n in Neighbor)
        {
            if (n.IsDone() == true)
            i++;
        }
        if(i>=3)
        {
            //abObj.ResetStructure();
           print("reset");
        }
        heightRecord = 0f;
    }

    public override void AgentOnDone()
    {

    }
}
