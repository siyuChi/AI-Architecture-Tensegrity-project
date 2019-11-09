//using MLAgents;
//using UnityEngine;
//using ZH.Tensegrity;

//public class TensigrityAdjustAgent : Agent
//{
//    [SerializeField] TensegrityObject AdjustObj;
//    [SerializeField] float minLength = 0.6f;
//    [SerializeField] float maxLength = 10f;
//    [SerializeField] public FindCloestPointtoTarget FindCloestPointtoTarget;
//    float distanceRecord = float.MaxValue;
//    private float length;
//    private Vector3 centerpos;
//    float distance;
//    private Vector3 cloPoint;
//    private Vector3 curPos;
//    private Vector3 prePos;
//    void SetUnitAsDefualt()
//    {
//        AdjustObj.SetAsDefualt();
//    }

//    public override void InitializeAgent()
//    {
        
//        FindCloestPointtoTarget.SetTobj(AdjustObj);
//        SetUnitAsDefualt();
//        distanceRecord = distance = FindCloestPointtoTarget.CaculateDistanceinColliders(AdjustObj);
//        cloPoint = FindCloestPointtoTarget.closestPoint;
//        curPos = prePos = AdjustObj.elements.center.transform.position;
//    }

//    public override void CollectObservations()
//    {
//        var tardir = (cloPoint - AdjustObj.elements.center.transform.position).normalized;

//        AddVectorObs(tardir);
//        AddVectorObs(AdjustObj.elements.center.transform.position);

//        foreach (var s in AdjustObj.elements.Strings)
//        {
//            AddVectorObs(s.transform.position);
//            AddVectorObs(s.m_ConfigurableJoint.currentForce);
//            AddVectorObs(s.m_ConfigurableJoint.currentTorque);
//            AddVectorObs(s.length);
//        }

//        foreach (var p in AdjustObj.elements.EndPoints)
//        {
//            AddVectorObs(p.position);
//        }
//    }

//    public override void AgentAction(float[] vectorAction, string textAction)
//    {
//        curPos = AdjustObj.elements.center.transform.position;
//        distance = FindCloestPointtoTarget.CaculateDistanceinColliders(AdjustObj);
//        SetReward(-0.005f);

//        var tardir = (cloPoint - AdjustObj.elements.center.transform.position).normalized;
//        var dir = (curPos - prePos).normalized;

//        AddReward(Vector3.Dot(tardir,dir) * 0.0000001f);
//        for (int i = 0; i < vectorAction.Length; i++)
//        {
//            AdjustObj.elements.Strings[i].GetCurlength(length);

//            float lengthparameter = GetParameter(vectorAction[i]);

//            AdjustObj.elements.Strings[i].SetCurLengthByParameter(length, lengthparameter);
//        }

       

//        float punish = distanceRecord-distance ;

//        AddReward(punish * 0.001f);
        
//        if (distance < distanceRecord-0.1f)
//        {
//            distanceRecord = distance;
//            AddReward(0.1f);
//            print(distanceRecord);
//        }


//        if (distanceRecord <1f)
//        {
//            print("touch");
//            SetReward(1f);
//            Done();
//        }

//        if (distance > distanceRecord + 1f)
//        {
//            print("bad");
//            SetReward(-1f);
//            Done();
//        }


//        if (curPos.y < 1f)
//        {
//            print("down");
//            SetReward(-1f);
//            Done();
//        }

//        if (IsMaxStepReached())
//        {
//            Done();
//        }
//       Monitor.Log("Reward", GetReward(), AdjustObj.elements.center.transform);

//        record previous
//        prePos = curPos;
//    }

//    public float GetParameter(float action) 
//    {
        
//        float a = Mathf.Clamp(action, -1f, 1f);
//        float a = Mathf.Sin(action);
//         a = a * 2f;
//         float t = a * 0.5f;//-5,5
//        return a;

//    }

//    public override void AgentReset()
//    {
//        AdjustObj.ResetStructure();
//        distanceRecord = float.MaxValue;
//    }

//    public override void AgentOnDone()
//    {

//    }
//}
