
using UnityEngine;
using ZH.Tensegrity.Support;
using ZH.Tensegrity;

public class NonlinearConnector : MonoBehaviour
{
    [SerializeField] TetrahedronTypeNonliner _type;
    [Header("Prefabs")]
    public PrefabContainer prefabs;
    [Header("Parameters")]
    public TensegrityParameters param;
    [Header("ConnectBar")] public NodePoint[] BarPoints=new NodePoint[8];
    [Header("ConnectString")]
    public NodePoint[] StringPointsFrom = new NodePoint[12];
    public NodePoint[] StringPointsTo = new NodePoint[24];

    public void BuildConnectString()
    {
        int index = 0;
        for (int n = 0; n < 3; n++)
        {
            var str0 = Instantiate(prefabs.m_StringPrefab, transform);
            str0.SetUp(StringPointsFrom[n], StringPointsTo[2*n], param.StringThickness);
            str0.index = index++;
            str0.name = $"String_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2*n].index}";
            _type.elements.Strings.Add(str0);
            print(str0.name);

            var str1 = Instantiate(prefabs.m_StringPrefab, transform);
            str1.SetUp(StringPointsFrom[n], StringPointsTo[2*n+1], param.StringThickness);
            str1.index = index++;
            str1.name = $"String_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2*n+1].index}";
            _type.elements.Strings.Add(str1);
            print(str1.name);
        }
       

   
    }

    public void BuildConnectBar()
    {
        int index = 0;
        for (int n = 0; n < 8; n+=2)
        {
            var b0 = Instantiate(prefabs.m_BarPrefab, transform);
            b0.SetUp(BarPoints[n], BarPoints[n+1], param.BarThickness);
            b0.index = index++;
            b0.name = $"Bar_{b0.index}_s{BarPoints[n].index}_e{BarPoints[n+1].index}";
            _type.elements.Bars.Add(b0);
            print(b0.name);
        }
        //0-5,2-0
        //0-10,2-4
        //4-4,2-10
        //2-5,4-10
    }




    // Start is called before the first frame update
    void Start()
    {
        BarPoints[0] = _type.elements.EndPoints[5];
        BarPoints[1] = _type.elements.EndPoints[24];
        BarPoints[2] = _type.elements.EndPoints[10];
        BarPoints[3] = _type.elements.EndPoints[28];
        BarPoints[4] = _type.elements.EndPoints[52];
        BarPoints[5] = _type.elements.EndPoints[34];
        BarPoints[6] = _type.elements.EndPoints[29];
        BarPoints[7] = _type.elements.EndPoints[48];

        StringPointsFrom[0] = _type.elements.EndPoints[21];
        StringPointsFrom[1] = _type.elements.EndPoints[22];
        StringPointsFrom[2] = _type.elements.EndPoints[23];

        StringPointsTo[0] = _type.elements.EndPoints[11];
        StringPointsTo[1] = _type.elements.EndPoints[8];
        StringPointsTo[2] = _type.elements.EndPoints[10];
        StringPointsTo[3] = _type.elements.EndPoints[5];
        StringPointsTo[4] = _type.elements.EndPoints[7];
        StringPointsTo[5] = _type.elements.EndPoints[3];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            BuildConnectString();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            BuildConnectBar();
        }
    }
}
