using System.Collections.Generic;
using System.Linq;
using MLAgents;
using UnityEngine;
using ZH.Tensegrity;

public class ElevenWalkAgent : Agent
{
    public AgentType agentType;
    private NeighborManager _neighborManager;
    [SerializeField] TensegrityObject eObj;
    [SerializeField] private GameObject centerPrefab;
    [SerializeField] float minLength = 0.6f;
    [SerializeField] float maxLength = 5f;
    [SerializeField] Transform target;
    [SerializeField] List<TensileElement> _String = new List<TensileElement>();
    [SerializeField] List<ElevenWalkAgent> Neighbor = new List<ElevenWalkAgent>();
    [SerializeField] List<GameObject> cenList = new List<GameObject>();
    private List<NodePoint> Pos = new List<NodePoint>();
    private List<NodePoint> Joints = new List<NodePoint>();
    [SerializeField]public GameObject center;
    private Rigidbody centerrb;
    float[] defaultCenLength = new float[11];
    List<TensileElement>[] stringGroup = new List<TensileElement>[10];
    public bool doneFlag = false;
    private Color defColor;

    public enum AgentType
    {
        StringA,
        StringB,
        StringC,
        StringD,
        StringE,
        StringF,
        StringG,
        StringH,
        StringI,
        StringJ,
        StringK,
        COString
    }
    float distanceRecord;
    float prevDistance;
    private float neighborreward;

    ///neighbor: Neighbor/cenlist
    public override void InitializeAgent()
    {
        _String.Clear();
        Pos.Clear();
        cenList.Clear();

            eObj.SetAsDefualt();
            //ResetTargetPosition();

            var S = eObj.elements.Strings;
            foreach (var s in S)
            {
                if (s.CompareTag(agentType.ToString()))
                {
                    _String.Add(s);
                }
            }
            SetPos();
        ///unbaked
        SetCenter();
        _neighborManager = GetComponentInParent<NeighborManager>();
        var neigh = _neighborManager.GetNeighbor();
        foreach (var n in neigh)
        {
            if(n.name != this.name)
            Neighbor.Add(n);
        }

        foreach (var n in Neighbor)
        {
            cenList.Add(n.center);
        }
        distanceRecord = prevDistance = Vector3.Distance(target.transform.position, center.transform.position);
        defColor = center.GetComponent<MeshRenderer>().material.color;
    }


    Vector3 ProjectedVector(Vector3 v,Vector3 normal)
    {
        return Vector3.ProjectOnPlane(v, normal);
    }

    public override void CollectObservations()
    {
        
            UpdateCenterPos();
            var d = target.transform.position - center.transform.position;

            var v = centerrb.velocity;

            float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

            AddVectorObs(dot);
            AddVectorObs(d);
            AddVectorObs(v);
            AddVectorObs(d.magnitude);

            
        


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


        foreach (var c in cenList)
        {
            AddVectorObs(c.transform);
           
        }

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
      
        //print("neighbor1 center position is"+ cenList[0]);
        if (doneFlag == false)
        {
        SetReward(-0.005f);
        
            for (int i = 0; i < vectorAction.Length; i++)
            {

                float length = GetLength(vectorAction[i]);

                _String[i].SetLength(length);
                var dir = target.position - center.transform.position;
                 _String[i].startPoint.AttatchedBar.GetComponent<Rigidbody>().AddForce(dir.normalized*0.01f,ForceMode.Impulse);

                //_String[i].startPoint.AttatchedBar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        
            var d = target.transform.position - center.transform.position;
            float dist = d.magnitude;
            float punish = prevDistance - dist;

            AddReward((punish) * 0.001f);

            var v = centerrb.velocity;

            float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

            AddReward(dot * 0.001f);

            //AddReward(v.magnitude*0.001f);


            if (dist < distanceRecord)
            {
                AddReward(0.001f);
                distanceRecord = dist;
            }
            if (dist < 10f)
            {
                //print(this.name+"good");
                SetReward(1f);
                Done();// freeze
                doneFlag = true;
            }

            if (dist - distanceRecord > 100f)
            {
               // print(this.name +"bad");
                SetReward(-1f);
                Done();
                doneFlag = true;
        }

            ////fall
            //if (center.transform.position.y < 15)
            //{
            //   // print(this.name +"down");
            //    SetReward(-1f);
            //    Done();
            //    doneFlag = true;
            //}
            prevDistance = dist;




                foreach (var n in Neighbor)//10 neighbors
                {
                    neighborreward += n.GetReward();
                }

                SetReward((GetReward()+neighborreward)/10);
                

           // Monitor.Log("Reward", GetReward(), center.transform);
            
        }

        //</color=#123>ugguygiugp</color>tffjb
        if (doneFlag == true)
        {
            center.GetComponent<MeshRenderer>().material.color = Color.red;
            center.GetComponent<MeshRenderer>().material.SetColor("_EMMISION",Color.red);
            center.GetComponent<MeshRenderer>().material.EnableKeyword("EMMISION");
        }
    }
    #region Centre method

    void SetCenter()
    {
        //center = Instantiate(centerPrefab, transform);
        //UpdateCenterPos();
        //center.transform.localScale = Vector3.one * 10f;
        //center.name = "center" + agentType.ToString();
        center = GetComponentInChildren<TensegrityCenter>().gameObject;
        centerrb = center.GetComponent<Rigidbody>();
    }

    void UpdateCenterPos()
    {
        Vector3 sum = Vector3.zero;

        foreach (var p in Pos)
        {
            sum += p.transform.position;
        }
        var averagePos = sum / Pos.Count;
        center.transform.position = averagePos;
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


    #endregion

    public float GetLength(float action)
    {
        float a = Mathf.Clamp(action, -1f, 1f);

        float t = (a + 1f) * 0.5f;

        return Mathf.Lerp(minLength, maxLength, t);

    }

    public override void AgentReset()
    {

        int i = 0;
        foreach (var n in Neighbor)
        {
            if (n.doneFlag == true)
            {
                i++;
            }
                
        }

        if (i >= 8)
        {
            print("reset");

            eObj.ResetStructure();
           // ResetTargetPosition();
            i = 0;
            doneFlag = false;
            center.GetComponent<MeshRenderer>().material.color = defColor;
            foreach (var n in Neighbor)
            {
                n.doneFlag = false;
            }
            

        }
        distanceRecord = prevDistance = Vector3.Distance(target.transform.position, center.transform.position);

       

    }

    public override void AgentOnDone()
    {

    }
}






//using System.Collections.Generic;
//using System.Linq;
//using MLAgents;
//using UnityEngine;
//using ZH.Tensegrity;

//public class ElevenWalkAgent : Agent
//{
//    public AgentType agentType;
//    private NeighborManager _neighborManager;
//    [SerializeField] TensegrityObject eObj;
//    [SerializeField] private GameObject centerPrefab;
//    [SerializeField] float minLength = 0.6f;
//    [SerializeField] float maxLength = 5f;
//    [SerializeField] Transform target;
//    [SerializeField] float targetRange = 10f;
//    [SerializeField] List<TensileElement> _String = new List<TensileElement>();
//    [SerializeField] List<ElevenWalkAgent> Neighbor = new List<ElevenWalkAgent>();
//    private List<NodePoint> Pos = new List<NodePoint>();
//    private List<NodePoint> Joints = new List<NodePoint>();
//    public GameObject center;
//    private Rigidbody centerrb;
//    float[] defaultCenLength = new float[11];
//    List<TensileElement>[] stringGroup = new List<TensileElement>[10];
//    public enum AgentType
//    {
//        StringA,
//        StringB,
//        StringC,
//        StringD,
//        StringE,
//        StringF,
//        StringG,
//        StringH,
//        StringI,
//        StringJ,
//        StringK,
//        COString
//    }
//    float distanceRecord;
//    float prevDistance;
//    private float neighborreward;

//    void ResetTargetPosition()
//    {
//        var p = Random.insideUnitCircle * targetRange;

//        target.transform.localPosition = new Vector3(p.x, target.transform.localPosition.y, p.y);

//    }




//    public override void InitializeAgent()
//    {


//        if (agentType != AgentType.COString)
//        {
//            eObj.SetAsDefualt();
//            ResetTargetPosition();

//            var S = eObj.elements.Strings;
//            foreach (var s in S)
//            {
//                if (s.CompareTag(agentType.ToString()))
//                {
//                    _String.Add(s);
//                }
//            }
//            AddJoint();
//            SetPos();
//            SetCenter();
//            _neighborManager = GetComponentInParent<NeighborManager>();
//            Neighbor = _neighborManager.GetNeighbor(this);
//            distanceRecord = prevDistance = Vector3.Distance(target.transform.position, center.transform.position);
//        }


//        if (agentType == AgentType.COString)
//        {

//            var S = eObj.elements.Strings;
//            foreach (var s in S)
//            {
//                if (s.CompareTag(agentType.ToString()))
//                {
//                    _String.Add(s);
//                }
//            }
//            for (int i = 0; i < 10; i++)
//            {
//                var partcostring = _String.Skip(i * 12).Take(12).ToList();
//                stringGroup[i] = partcostring;
//            }

//        }


//    }
//    private void AddJoint()
//    {

//        foreach (var a in _String)
//        {
//            var EP = a.EndPt;
//            var SP = a.StartPt;

//            if (!Joints.Contains(SP))
//            {
//                Joints.Add(SP);
//            }
//            if (!Joints.Contains(EP))
//            {
//                Joints.Add(EP);
//            }
//        }
//        Joints.Distinct();
//    }

//    Vector3 ProjectedVector(Vector3 v, Vector3 normal)
//    {
//        return Vector3.ProjectOnPlane(v, normal);
//    }

//    public override void CollectObservations()
//    {
//        if (agentType != AgentType.COString)
//        {
//            UpdateCenterPos();
//            var d = target.transform.position - center.transform.position;

//            var v = centerrb.velocity;

//            float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

//            AddVectorObs(dot);
//            AddVectorObs(d);
//            AddVectorObs(v);
//            AddVectorObs(d.magnitude);

//            foreach (var n in Neighbor)
//            {
//                if (n != null)
//                    AddVectorObs(n.center.transform);
//            }

//        }


//        foreach (var s in _String)
//        {
//            AddVectorObs(s.transform.position);
//            AddVectorObs(s.m_ConfigurableJoint.currentForce);
//            AddVectorObs(s.m_ConfigurableJoint.currentTorque);
//            AddVectorObs(s.length);
//        }

//        foreach (var p in Pos)
//        {
//            AddVectorObs(p.position);
//        }


//    }

//    public override void AgentAction(float[] vectorAction, string textAction)
//    {
//        SetReward(-0.005f);

//        if (Neighbor != null)
//        {
//            foreach (var n in Neighbor)
//            {
//                neighborreward += n.GetReward();
//            }
//            AddReward(neighborreward * 0.1f);
//        }


//        if (agentType != AgentType.COString)
//        {

//            for (int i = 0; i < vectorAction.Length; i++)
//            {
//                float length = GetLength(vectorAction[i]);

//                _String[i].SetLength(length);
//            }
//            var d = target.transform.position - center.transform.position;
//            float dist = d.magnitude;
//            float punish = prevDistance - dist;

//            AddReward((punish) * 0.01f);

//            var v = centerrb.velocity;

//            float dot = Vector3.Dot(ProjectedVector(v, Vector3.up), ProjectedVector(d, Vector3.up).normalized);

//            AddReward(dot * 0.01f);

//            //AddReward(v.magnitude*0.001f);


//            if (dist < distanceRecord)
//            {
//                AddReward(0.1f);
//                distanceRecord = dist;
//            }
//            if (dist < 5f)
//            {
//                print("good");
//                SetReward(1f);
//                Done();// freeze
//            }

//            if (dist - distanceRecord > 100f)
//            {
//                print("bad");
//                SetReward(-1f);
//                Done();
//            }
//            //fall
//            if (center.transform.position.y < 18)
//            {
//                SetReward(-1f);
//                Done();
//            }
//            prevDistance = dist;

//            Monitor.Log("Reward", GetReward(), center.transform);
//        }
//    }

//    #region Centre method

//    void SetCenter()
//    {
//        center = Instantiate(centerPrefab, transform);
//        UpdateCenterPos();
//        center.transform.localScale = Vector3.one * 10f;
//        center.name = "center" + agentType.ToString();
//        centerrb = center.GetComponent<Rigidbody>();
//    }

//    void UpdateCenterPos()
//    {
//        Vector3 sum = Vector3.zero;

//        foreach (var p in Pos)
//        {
//            sum += p.transform.position;
//        }
//        var averagePos = sum / Pos.Count;
//        center.transform.position = averagePos;
//    }

//    void SetPos()
//    {
//        Pos.Clear();

//        foreach (var a in _String)
//        {
//            var EP = a.EndPt;
//            var SP = a.StartPt;
//            //Vector3 Epos = EP.position;
//            //Vector3 Spos = SP.position;

//            if (!Pos.Contains(EP))
//            {
//                Pos.Add(EP);
//            }

//            if (!Pos.Contains(SP))
//            {
//                Pos.Add(SP);
//            }
//        }

//        Pos.Distinct();
//        print(Pos.Count);

//    }


//    #endregion

//    public float GetLength(float action)
//    {
//        float a = Mathf.Clamp(action, -1f, 1f);

//        float t = (a + 1f) * 0.5f;

//        return Mathf.Lerp(minLength, maxLength, t);

//    }

//    public override void AgentReset()
//    {
//        if (agentType != AgentType.COString)
//        {
//            int i = 0;
//            foreach (var n in Neighbor)
//            {
//                if (n.IsDone() == true)
//                    i++;
//            }

//            if (i >= 3)
//            {
//                eObj.ResetStructure();
//                ResetTargetPosition();

//            }
//            distanceRecord = prevDistance = Vector3.Distance(target.transform.position, center.transform.position);
//        }

//    }

//    public override void AgentOnDone()
//    {

//    }
//}
