using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class TensigrityWalkAgent : Agent
{
    [SerializeField] TensegrityObject tObj;
    [SerializeField] float minLength = 0.6f;
    [SerializeField] float maxLength = 5f;
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 10f;

    float distanceRecord;
    float prevDistance;

    void ResetTargetPosition()
    {
        //var p = Random.insideUnitCircle * targetRange;

        //target.transform.localPosition = new Vector3(p.x, target.transform.localPosition.y, p.y);

    }


    

    public override void InitializeAgent()
    {
        tObj.SetAsDefualt();
        ResetTargetPosition();

    }

    Vector3 ProjectedVector(Vector3 v,Vector3 normal)
    {
        return Vector3.ProjectOnPlane(v, normal);
    }

    public override void CollectObservations()
    {
        var d = target.transform.position - tObj.elements.center.transform.position;

        var v = tObj.elements.center.GetVelocity();

        float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

        AddVectorObs(dot);
        AddVectorObs(d);
        AddVectorObs(v);
        AddVectorObs(d.magnitude);

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

            ///
            ///
            var dir = target.position - tObj.elements.center.transform.position;
            tObj.elements.Strings[i].startPoint.AttatchedBar.GetComponent<Rigidbody>().AddForce(dir.normalized * 0.03f, ForceMode.Impulse);
        }
        var d = target.transform.position - tObj.elements.center.transform.position;
        float dist = d.magnitude;
        float punish = prevDistance-dist;

        AddReward((punish)*0.01f);

        var v = tObj.elements.center.GetVelocity();

        float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

        AddReward(dot * 0.01f);

        //AddReward(v.magnitude*0.001f);


        if (dist < distanceRecord)
        {
            AddReward(0.1f);
            distanceRecord = dist;
        }
        if (dist < 0.5f)
        {
            print("good");
            SetReward(1f);
            Done();
        }

        if ( dist-distanceRecord > 3f)
        {
            print("bad");
            SetReward(-1f);
            Done();
        }

        prevDistance = dist;

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
        tObj.ResetStructure();
        ResetTargetPosition();
        distanceRecord =prevDistance= Vector3.Distance(target.transform.position, tObj.elements.center.transform.position);
    }

    public override void AgentOnDone()
    {

    }
}
