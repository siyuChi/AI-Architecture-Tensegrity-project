using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class TensigrityPushUpAgent : Agent
{
    [SerializeField] TensegrityObject tObj;
    [SerializeField] float minLength = 0.6f;
    [SerializeField] float maxLength = 5f;


    float heightRecord;

    //public void BuildTensegrity()
    //{
    //    tObj.SetupStructure();
    //    tObj.ApplyPhysics();
    //    LooseStrings();
    //}

    void SetUnitAsDefualt()
    {
        tObj.SetAsDefualt();
    }

    //void LooseStrings()
    //{
    //    for (int i = 0; i < tObj.elements.Strings.Count; i++)
    //    {
    //        var str = tObj.elements.Strings[i];
    //        str.SetLengthByParameter(maxLength);

    //        //str.SetLength(maxLengthParam);
    //    }
    //}
    public override void InitializeAgent()
    {
        //BuildTensegrity();

        SetUnitAsDefualt();
    }

    public override void CollectObservations()
    {
        AddVectorObs(tObj.elements.center.transform.position.y);

        foreach (var s in tObj.elements.Strings)
        {
            AddVectorObs(s.transform.position);
            AddVectorObs(s.m_ConfigurableJoint.currentForce);
            AddVectorObs(s.m_ConfigurableJoint.currentTorque);
            AddVectorObs(s.length);
        }

        foreach (var p in tObj.elements.EndPoints)
        {
            AddVectorObs(p.position);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        SetReward(-0.005f);

        for (int i = 0; i < vectorAction.Length; i++)
        {
            float length = GetLength(vectorAction[i]);

            tObj.elements.Strings[i].SetLength(length);
        }

        float height = tObj.elements.center.transform.position.y;
        float punish = height - heightRecord;

        AddReward((height+punish)*0.001f);


        if (height > heightRecord)
        {
            AddReward(0.1f);
            heightRecord = height;
        }
        if (height > 2f)
        {
            print("good");
            SetReward(1f);
            Done();
        }

        if (heightRecord - height > 1f)
        {
            print("bad");
            SetReward(-1f);
            Done();
        }

        Monitor.Log("Reward", GetReward(), tObj.elements.center.transform);
    }

    public float GetLength(float action)
    {
        float a = Mathf.Clamp(action, -1f, 1f);

        float t = (a + 1f) * 0.5f;

        return Mathf.Lerp(minLength, maxLength, t);

    }

    public override void AgentReset()
    {
        //tObj.ResetStructure();
        //LooseStrings();
        heightRecord = 0f;
    }

    public override void AgentOnDone()
    {

    }
}
