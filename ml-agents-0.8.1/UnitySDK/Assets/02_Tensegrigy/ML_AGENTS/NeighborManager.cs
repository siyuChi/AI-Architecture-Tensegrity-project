using System.Collections;
using System.Collections.Generic;
using SpatialSlur.Core;
using UnityEngine;

public class NeighborManager : MonoBehaviour
{
    private Transform[] allLayers;
    [SerializeField] private List<ElevenWalkAgent> Neighbor = new List<ElevenWalkAgent>();
    [SerializeField] List<GameObject> cenList = new List<GameObject>();
    void Awake()
    {


        //allLayers = GetComponentsInChildren<Transform>();

        //foreach (var la in allLayers)
        //{
        //    var taragent = la.gameObject.GetComponent<ElevenWalkAgent>();
        //    if (taragent != null)//&& taragent.name != "TensegrityAgentCO"
        //    {
        //        Neighbor.Add(taragent);
        //    }
        //}
    }
    public List<ElevenWalkAgent> GetNeighbor() //ElevenWalkAgent thisAgent
    {
        var neighbor = Neighbor;

        return neighbor;
    }


    //neighbor.Remove(thisAgent);
    void Start()
    {
        cenList = GetAllCenter();
    }

    public List<GameObject> GetAllCenter()
    {
        List<GameObject> cenList = new List<GameObject>();
        foreach (var n in Neighbor)
        {
            var cen = n.center;
            cenList.Add(cen);
        }

        return cenList;
    }
    public List<ElevenWalkAgent> GetTotalAgentList
    {
        get { return Neighbor; }
    } 


    public float[] GetCenterLength()
    {
        float[] defaultLength = new float[11];

        for (int i = 0; i < 10; i++)
        {
            var center1 = GetAllCenter()[i];
            var center2 = GetAllCenter()[i + 1];
            defaultLength[i] = Vector3.Distance(center1.transform.position, center2.transform.position);
        }

        return defaultLength;
    }
}
