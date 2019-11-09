using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZH.Tensegrity;

public class DrawLine: MonoBehaviour
{
    //[SerializeField]Tensegrity Agent;
    //TargetFrame frame;

    //void OnDrawGizmos()
    //{
    //    frame = Agent.target;

    //    var line = frame.FindLine();

    //    Gizmos.color = Color.blue;

    //    Gizmos.DrawLine(line[0], line[1]);
    //}
    [SerializeField] private ElevenTetraType _Obj;
    List<Vector3[]> _NonlinearTetra = new List<Vector3[]>();

    void Awake()
    {
        _Obj = gameObject.GetComponent<ElevenTetraType>();
        _NonlinearTetra = _Obj.NonlinearTetra;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Vector3.back*100,Vector3.down*100);
        //Gizmos.DrawCube(new Vector3(0,0,0),new Vector3(100,100,100));
        for (int i = 0; i < _NonlinearTetra.Count; i++)
        {
            for (int k = 0; k < _NonlinearTetra[i].Length; k++)
            {
                var p0 = _NonlinearTetra[i][k];

                for (int j = 0; j < _NonlinearTetra[i].Length; j++)
                {
                    var p1 = _NonlinearTetra[i][j];
                    if (j > k)
                    {
                        Gizmos.DrawLine(p0, p1);

                    }

                }
            }
        }
    }
}
