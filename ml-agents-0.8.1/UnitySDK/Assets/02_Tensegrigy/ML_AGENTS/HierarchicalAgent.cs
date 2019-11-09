using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class HierarchicalAgent : Agent
{
    [SerializeField] private Agent low_level_agent;
    [SerializeField] private List<Brain> skills;

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


    public override void InitializeAgent()
    {
        low_level_agent.agentParameters.onDemandDecision = true;
    }

    public override void CollectObservations()
    {
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        int action = 1; // read this from vector action
        low_level_agent.GiveBrain(skills[action]);
        low_level_agent.RequestDecision(); // asking low_level_agent to act
        
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
