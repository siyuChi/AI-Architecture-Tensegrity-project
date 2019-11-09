using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class ElevenWalkCostringAgent : Agent
{
    //private NeighborManager _neighborManager;
    [SerializeField] TensegrityObject eObj;
    [SerializeField] float minLength = 0.6f;
    [SerializeField] float maxLength = 5f;
    [SerializeField] List<TensileElement> _String = new List<TensileElement>();
    [SerializeField] List<ElevenWalkAgent> Neighbor = new List<ElevenWalkAgent>();
    private List<NodePoint> Pos = new List<NodePoint>();
    private List<NodePoint> Joints = new List<NodePoint>();

    float[] defaultCenLength = new float[11];
    List<TensileElement>[] stringGroup = new List<TensileElement>[10]; 

    float distanceRecord;
    float prevDistance;
    private float neighborreward;


    public override void InitializeAgent()
    {

            var S = eObj.elements.Strings;
            foreach (var s in S)
            {
                if (s.CompareTag("COString"))
                {
                    _String.Add(s);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                var partcostring = _String.Skip(i * 12).Take(12).ToList();
                stringGroup[i] = partcostring;
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

    Vector3 ProjectedVector(Vector3 v,Vector3 normal)
    {
        return Vector3.ProjectOnPlane(v, normal);
    }

    public override void CollectObservations()
    {


        foreach (var s in _String)
        {
            AddVectorObs(s.transform.position);
            AddVectorObs(s.m_ConfigurableJoint.currentForce);
            AddVectorObs(s.m_ConfigurableJoint.currentTorque);
            AddVectorObs(s.length);
        }

        foreach (var p in Pos)
        {
            AddVectorObs(p.position);
        }

       
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        SetReward(-0.005f);

        if (Neighbor != null)
        {
            foreach (var n in Neighbor)
            {
                neighborreward += n.GetReward();
            }
            AddReward(neighborreward * 0.1f);
        }


            for (int i = 0; i < vectorAction.Length; i++)
            {
                float length = GetLength(vectorAction[i]);
                foreach (var s in stringGroup[i])
                {
                    s.SetLength(length);
                }

               // defaultCenLength[i] = _neighborManager.GetCenterLength()[i];

            }

    }


    void SetPos()
    {
        Pos.Clear();

        foreach (var a in _String)
        {
            var EP = a.EndPt;
            var SP = a.StartPt;
            //Vector3 Epos = EP.position;
            //Vector3 Spos = SP.position;

            if (!Pos.Contains(EP))
            {
                Pos.Add(EP);
            }

            if (!Pos.Contains(SP))
            {
                Pos.Add(SP);
            }
        }

        Pos.Distinct();
        print(Pos.Count);

    }

    public float GetLength(float action)
    {
        float a = Mathf.Clamp(action, -1f, 1f);

        float t = (a + 1f) * 0.5f;

        return Mathf.Lerp(minLength, maxLength, t);

    }

    public override void AgentReset()
    {

        
    }

    public override void AgentOnDone()
    {

    }
}
