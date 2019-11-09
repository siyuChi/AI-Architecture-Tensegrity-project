using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ZH.Tensegrity
{
    public class ElevenTetraType : TensegrityObject
    {

        public List<Vector3[]> NonlinearTetra=new List<Vector3[]>();
        private Vector3[] SingleTetra=new Vector3[4];

        [Header("CustomParam")]
        public float radius = 3f;
        [Range(0, 1f)] public float offsetParameter = 0.8f;

        [Header("ConnectString")]
        public NodePoint[] StringPointsFrom = new NodePoint[60];
        public NodePoint[] StringPointsTo = new NodePoint[60];

        public override void SetupPoints()
        {
            SetTetraFrame();
            for (int n = 0; n < NonlinearTetra.Count; n++)
            {
                if (n==0||n==2||n==4 || n == 6 || n == 8 || n == 10)
                {
                    for (int i = 0; i < NonlinearTetra[n].Length; i++)
                    {
                        int to = i;
                        int from1 = i + 1 < NonlinearTetra[n].Length - 1 ? i + 1 : 0;
                        int from2 = from1 + 1 < NonlinearTetra[n].Length - 1 ? from1 + 1 : 0;
                        int from3 = i < NonlinearTetra[n].Length - 1 ? 3 : from2 + 1;
                        var p0 = NonlinearTetra[n][to];
                        var p1 = NonlinearTetra[n][from1];
                        var p2 = NonlinearTetra[n][from2];
                        var p3 = NonlinearTetra[n][from3];

                        var d1 = p0 - p1;
                        var d2 = p0 - p2;
                        var d3 = p0 - p3;
                        elements.ConstructionPoints.Add(offsetParameter * d1 + p1);
                        elements.ConstructionPoints.Add(offsetParameter * d2 + p2);
                        elements.ConstructionPoints.Add(offsetParameter * d3 + p3);
                    }
                    int index = 0;
                    for (int i = elements.ConstructionPoints.Count-12; i < elements.ConstructionPoints.Count; i++)
                    {
                        elements.ConstructionPoints[i] = TensegrityUtil.CleanUp(elements.ConstructionPoints[i]);
                        var p = elements.ConstructionPoints[i];
                        var ep = Instantiate(prefabs.m_PointPrefab, transform);
                        ep.transform.localPosition = p;
                        ep.transform.localScale = Vector3.one * param.PointSize * 30;
                        ep.name = "endpoint_" +n+ "_" + index;
                        ep.index = index;
                        if (ep.GetComponentInChildren<Text>())
                        {
                            ep.GetComponentInChildren<Text>().text = $"{index}";
                        }
                        ep.UpdatePosition();
                        elements.EndPoints.Add(ep);
                        index++;
                    }
                    elements.center = Instantiate(prefabs.m_Center, transform);
                    elements.center.defaultPosition = transform.position;
                    elements.center.UpdateCenter(this);
                }
                if (n == 1 || n == 3 || n == 5 || n == 7 || n == 9)
                {
                    for (int i = 0; i < NonlinearTetra[n].Length; i++)
                    {
                        int to = i;
                        int from1 = i + 1 < NonlinearTetra[n].Length - 1 ? i + 1 : 0;
                        int from2 = from1 + 1 < NonlinearTetra[n].Length - 1 ? from1 + 1 : 0;
                        int from3 = i < NonlinearTetra[n].Length - 1 ? 3 : from2 + 1;
                        var p0 = NonlinearTetra[n][to];
                        var p1 = NonlinearTetra[n][from1];
                        var p2 = NonlinearTetra[n][from2];
                        var p3 = NonlinearTetra[n][from3];
                        var cen = (p1 + p2 + p3) / 3;
                        var d1 = cen - p1;
                        var d2 = cen - p2;
                        var d3 = cen - p3;
                        elements.ConstructionPoints.Add((1f - offsetParameter) * d1 + cen);
                        elements.ConstructionPoints.Add((1f - offsetParameter) * d2 + cen);
                        elements.ConstructionPoints.Add((1f - offsetParameter) * d3 + cen);
                    }
                    int index = 0;
                    for (int i = elements.ConstructionPoints.Count - 12; i < elements.ConstructionPoints.Count; i++)
                    {
                        elements.ConstructionPoints[i] = TensegrityUtil.CleanUp(elements.ConstructionPoints[i]);
                        var p = elements.ConstructionPoints[i];
                        var ep = Instantiate(prefabs.m_PointPrefab, transform);
                        ep.transform.localPosition = p;
                        ep.transform.localScale = Vector3.one * param.PointSize * 30;
                        ep.name = "endpoint_" + n + "_" + index;
                        ep.index = index;
                        if (ep.GetComponentInChildren<Text>())
                        {
                            ep.GetComponentInChildren<Text>().text = $"{index}";
                        }
                        ep.UpdatePosition();
                        elements.EndPoints.Add(ep);
                        index++;
                    }
                    elements.center = Instantiate(prefabs.m_Center, transform);
                    elements.center.defaultPosition = transform.position;
                    elements.center.UpdateCenter(this);
                }


             
            }
            print(elements.EndPoints.Count);
            #region connect

            StringPointsFrom[0] = elements.EndPoints[21];
            StringPointsFrom[2] = elements.EndPoints[22];
            StringPointsFrom[4] = elements.EndPoints[23];

            StringPointsFrom[6] = elements.EndPoints[15];
            StringPointsFrom[8] = elements.EndPoints[16];
            StringPointsFrom[10] = elements.EndPoints[17];

            StringPointsFrom[12] = elements.EndPoints[45];
            StringPointsFrom[14] = elements.EndPoints[46];
            StringPointsFrom[16] = elements.EndPoints[47];

            StringPointsFrom[18] = elements.EndPoints[39];
            StringPointsFrom[20] = elements.EndPoints[40];
            StringPointsFrom[22] = elements.EndPoints[41];


            //no.2
            StringPointsFrom[1] = elements.EndPoints[14];
            StringPointsFrom[3] = elements.EndPoints[17];
            StringPointsFrom[5] = elements.EndPoints[20];

            StringPointsFrom[7] = elements.EndPoints[12];
            StringPointsFrom[9] = elements.EndPoints[19];
            StringPointsFrom[11] = elements.EndPoints[22];

            StringPointsFrom[13] = elements.EndPoints[38];
            StringPointsFrom[15] = elements.EndPoints[41];
            StringPointsFrom[17] = elements.EndPoints[44];

            StringPointsFrom[19] = elements.EndPoints[36];
            StringPointsFrom[21] = elements.EndPoints[43];
            StringPointsFrom[23] = elements.EndPoints[46];

            ///
            StringPointsFrom[24] = elements.EndPoints[69];
            StringPointsFrom[25] = elements.EndPoints[62];
            StringPointsFrom[26] = elements.EndPoints[70];
            StringPointsFrom[27] = elements.EndPoints[65];
            StringPointsFrom[28] = elements.EndPoints[71];
            StringPointsFrom[29] = elements.EndPoints[68];

            StringPointsFrom[30] = elements.EndPoints[60];
            StringPointsFrom[31] = elements.EndPoints[64];
            StringPointsFrom[32] = elements.EndPoints[61];
            StringPointsFrom[33] = elements.EndPoints[66];
            StringPointsFrom[34] = elements.EndPoints[62];
            StringPointsFrom[35] = elements.EndPoints[69];

            StringPointsFrom[36] = elements.EndPoints[93];
            StringPointsFrom[37] = elements.EndPoints[86];
            StringPointsFrom[38] = elements.EndPoints[94];
            StringPointsFrom[39] = elements.EndPoints[89];
            StringPointsFrom[40] = elements.EndPoints[95];
            StringPointsFrom[41] = elements.EndPoints[92];

            StringPointsFrom[42] = elements.EndPoints[87];
            StringPointsFrom[43] = elements.EndPoints[91];
            StringPointsFrom[44] = elements.EndPoints[88];
            StringPointsFrom[45] = elements.EndPoints[84];
            StringPointsFrom[46] = elements.EndPoints[89];
            StringPointsFrom[47] = elements.EndPoints[94];

            StringPointsFrom[48] = elements.EndPoints[117];
            StringPointsFrom[49] = elements.EndPoints[110];
            StringPointsFrom[50] = elements.EndPoints[118];
            StringPointsFrom[51] = elements.EndPoints[113];
            StringPointsFrom[52] = elements.EndPoints[119];
            StringPointsFrom[53] = elements.EndPoints[116];

            StringPointsFrom[54] = elements.EndPoints[111];
            StringPointsFrom[55] = elements.EndPoints[115];
            StringPointsFrom[56] = elements.EndPoints[112];
            StringPointsFrom[57] = elements.EndPoints[108];
            StringPointsFrom[58] = elements.EndPoints[113];
            StringPointsFrom[59] = elements.EndPoints[118];


            for (int i = 0; i < 60; i++)
            {
                elements.EndPoints.Add(StringPointsFrom[i]);
            }


            StringPointsTo[0] = elements.EndPoints[11];
            StringPointsTo[1] = elements.EndPoints[8];
            StringPointsTo[2] = elements.EndPoints[10];
            StringPointsTo[3] = elements.EndPoints[5];
            StringPointsTo[4] = elements.EndPoints[7];
            StringPointsTo[5] = elements.EndPoints[3];

            StringPointsTo[6] = elements.EndPoints[25];
            StringPointsTo[7] = elements.EndPoints[30];
            StringPointsTo[8] = elements.EndPoints[27];
            StringPointsTo[9] = elements.EndPoints[31];
            StringPointsTo[10] = elements.EndPoints[28];
            StringPointsTo[11] = elements.EndPoints[24];

            StringPointsTo[12] = elements.EndPoints[35];
            StringPointsTo[13] = elements.EndPoints[32];
            StringPointsTo[14] = elements.EndPoints[29];
            StringPointsTo[15] = elements.EndPoints[34];
            StringPointsTo[16] = elements.EndPoints[27];
            StringPointsTo[17] = elements.EndPoints[31];

            StringPointsTo[18] = elements.EndPoints[49];
            StringPointsTo[19] = elements.EndPoints[54];
            StringPointsTo[20] = elements.EndPoints[55];
            StringPointsTo[21] = elements.EndPoints[51];
            StringPointsTo[22] = elements.EndPoints[48];
            StringPointsTo[23] = elements.EndPoints[52];

            ///////////
            StringPointsTo[24] = elements.EndPoints[59];
            StringPointsTo[25] = elements.EndPoints[56];
            StringPointsTo[26] = elements.EndPoints[57];
            StringPointsTo[27] = elements.EndPoints[50];
            StringPointsTo[28] = elements.EndPoints[49];
            StringPointsTo[29] = elements.EndPoints[54];

            StringPointsTo[30] = elements.EndPoints[79];
            StringPointsTo[31] = elements.EndPoints[75];
            StringPointsTo[32] = elements.EndPoints[73];
            StringPointsTo[33] = elements.EndPoints[78];
            StringPointsTo[34] = elements.EndPoints[72];
            StringPointsTo[35] = elements.EndPoints[76];

            StringPointsTo[36] = elements.EndPoints[83];
            StringPointsTo[37] = elements.EndPoints[80];
            StringPointsTo[38] = elements.EndPoints[77];
            StringPointsTo[39] = elements.EndPoints[82];
            StringPointsTo[40] = elements.EndPoints[75];
            StringPointsTo[41] = elements.EndPoints[79];

            StringPointsTo[42] = elements.EndPoints[97];
            StringPointsTo[43] = elements.EndPoints[102];
            StringPointsTo[44] = elements.EndPoints[99];
            StringPointsTo[45] = elements.EndPoints[103];
            StringPointsTo[46] = elements.EndPoints[96];
            StringPointsTo[47] = elements.EndPoints[100];

            StringPointsTo[48] = elements.EndPoints[104];
            StringPointsTo[49] = elements.EndPoints[107];
            StringPointsTo[50] = elements.EndPoints[101];
            StringPointsTo[51] = elements.EndPoints[106];
            StringPointsTo[52] = elements.EndPoints[99];
            StringPointsTo[53] = elements.EndPoints[103];

            StringPointsTo[54] = elements.EndPoints[121];
            StringPointsTo[55] = elements.EndPoints[126];
            StringPointsTo[56] = elements.EndPoints[123];
            StringPointsTo[57] = elements.EndPoints[127];
            StringPointsTo[58] = elements.EndPoints[120];
            StringPointsTo[59] = elements.EndPoints[124];


            ////
            for (int i = 0; i < 60; i++)
            {
                elements.EndPoints.Add(StringPointsTo[i]);
            }

            #endregion

        }


        public override void SetupBars()
        {
            for (int n = 0; n < NonlinearTetra.Count; n++)
            {
                if (n == 0 || n == 1 || n == 2 || n == 3 || n == 4 || n == 5 || n == 6 || n == 7 || n == 8 || n == 9|| n == 10 )
                {
                    List<Vector3> conPoints = elements.ConstructionPoints.Skip(n * 12).Take(12).ToList();
                    List<NodePoint> endPoints = elements.EndPoints.Skip(n * 12).Take(12).ToList();
                    int size = 3;
                    int index = 0;
                    var top = endPoints.GetRange(conPoints.Count - 3, 3);

                    for (int i = 0, p = 0; i < NonlinearTetra[n].Length - 1; i++, p += 3)
                    {
                        int topIndex = i + 1 < 3 ? i + 1 : 0;
                        int to = p - 3 < 0 ? conPoints.Count - 6 : p - 3;

                        var toGroup = endPoints.GetRange(to, size);

                        var fromGroup = endPoints.GetRange(p, size);
                        List<int> abd = new List<int>();

                        var ep0 = fromGroup[0];
                        var ep1 = toGroup[2];

                        var b0 = Instantiate(prefabs.m_BarPrefab, transform);

                        b0.SetUp(ep0, ep1, param.BarThickness);

                        b0.index = index++;
                        b0.name = $"Bar_{b0.index}_s{ep0.index}_e{ep1.index}";

                        elements.Bars.Add(b0);


                        var ep2 = fromGroup[1];

                        var ep3 = top[topIndex];

                        var b1 = Instantiate(prefabs.m_BarPrefab, transform);


                        b1.SetUp(ep2, ep3, param.BarThickness);

                        b1.index = index++;
                        b1.name = $"Bar_{b1.index}_s{ep2.index}_e{ep3.index}";

                        elements.Bars.Add(b1);

                    }
                }

            }
        }


        public override void SetupStrings()
        {
            for (int n = 0; n < NonlinearTetra.Count; n++)
            {
                if (n == 0 || n == 1 || n == 2 || n == 3 || n == 4 || n == 5 || n == 6 || n == 7 || n == 8 || n == 9 || n == 10)
                {
                    List<Vector3> conPoints = elements.ConstructionPoints.Skip(n * 12).Take(12).ToList();
                    List<NodePoint> endPoints = elements.EndPoints.Skip(n * 12).Take(12).ToList();
                    int index = 0;
                    int topIndex = 0;
                    var top = endPoints.GetRange(conPoints.Count - 3, 3);
                    for (int i = 0; i < conPoints.Count; i += 3)
                    {
                        var corner = endPoints.GetRange(i, 3);

                        for (int j = 0; j < 3; j++)
                        {
                            int fromPt = j;
                            int toPt = j + 1 < 3 ? j + 1 : 0;

                            var ep0 = corner[fromPt];
                            var ep1 = corner[toPt];

                            var str = Instantiate(prefabs.m_StringPrefab, transform);
                            str.SetUp(ep0, ep1, param.StringThickness);
                            str.index = index++;
                            str.name = $"String_{str.index}_s{ep0.index}_e{ep1.index}";
                            elements.Strings.Add(str);
                        }

                        if (i < endPoints.Count - 3)
                        {
                            int fromPt1 = 2;
                            int toPt1 = topIndex;
                            var ep2 = corner[fromPt1];
                            var ep3 = top[topIndex];

                            var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                            str1.SetUp(ep2, ep3, param.StringThickness);
                            str1.index = index++;
                            str1.name = $"String_{str1.index}_s{ep2.index}_e{ep3.index}";
                            elements.Strings.Add(str1);

                            topIndex++;

                            int fromPt2 = 0;
                            int toPt2 = i + 4 < endPoints.Count - 3 ? i + 4 : 1;
                            var ep4 = corner[fromPt2];
                            var ep5 = endPoints[toPt2];

                            var str2 = Instantiate(prefabs.m_StringPrefab, transform);
                            str2.SetUp(ep4, ep5, param.StringThickness);
                            str2.index = index++;
                            str2.name = $"String_{str2.index}_s{ep4.index}_e{ep5.index}";
                            elements.Strings.Add(str2);
                        }

                    }
                }
        
            }
            
            #region connect
            int index1 = 0;
            for (int n = 0; n < 60; n++)
            {
                if (n % 2 == 0)
                {

                    var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                    str0.SetUp(StringPointsFrom[n], StringPointsTo[n], param.StringThickness);
                    str0.index = index1++;
                    str0.name = $"CoString_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[n].index}";
                    // print(str0.name);
                    elements.Strings.Add(str0);

                    var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                    str1.SetUp(StringPointsFrom[n], StringPointsTo[n + 1], param.StringThickness);
                    str1.index = index1++;
                    str1.name = $"CoString_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[n + 1].index}";
                    // print(str1.name);
                    elements.Strings.Add(str1);

                }

                if (n % 2 == 1)
                {
                    var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                    str1.SetUp(StringPointsFrom[n], StringPointsTo[n - 1], param.StringThickness);
                    str1.index = index1++;
                    str1.name = $"CoString_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[n - 1].index}";
                    // print(str1.name);
                    elements.Strings.Add(str1);

                    var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                    str0.SetUp(StringPointsFrom[n], StringPointsTo[n], param.StringThickness);
                    str0.index = index1++;
                    str0.name = $"CoString_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[n].index}";
                    // print(str0.name);
                    elements.Strings.Add(str0);



                }
            }
            #endregion

        }



        public void SetTetraFrame()
        {
            Vector3 p1=new Vector3(0,105,-60);
            Vector3 p2=new Vector3(0,105,60);
            Vector3 p3=new Vector3(0,0,0);
            Vector3 p4=new Vector3(100,70,0);
            Vector3 p5=new Vector3(65,15,100);
            Vector3 p6=new Vector3(100,125,110);
            Vector3 p7=new Vector3(180,35,80);
            Vector3 p8=new Vector3(200, 180, 20);
            Vector3 p9=new Vector3(225,75,-20);
            Vector3 p10=new Vector3(300,100,75);
            Vector3 p11=new Vector3(310,155,- 35);
            Vector3 p12=new Vector3(300,230,65);
            Vector3 p13=new Vector3(400,155,55);
            Vector3 p14=new Vector3(350,165,165);
            //11 tetra
            NonlinearTetra.Add(new Vector3[]{p1,p2,p3,p4});
            NonlinearTetra.Add(new Vector3[]{p2,p3,p4,p5});
            NonlinearTetra.Add(new Vector3[]{p2,p4,p5,p6});
            NonlinearTetra.Add(new Vector3[]{p4,p5,p6,p7});
            NonlinearTetra.Add(new Vector3[]{p4,p6,p7,p8});
            NonlinearTetra.Add(new Vector3[]{p4,p7,p8,p9});
            NonlinearTetra.Add(new Vector3[]{p7,p8,p9,p10});
            NonlinearTetra.Add(new Vector3[]{p8,p9,p10,p11});
            NonlinearTetra.Add(new Vector3[]{p8,p10,p11,p12});
            NonlinearTetra.Add(new Vector3[]{p10,p11,p12,p13});
            NonlinearTetra.Add(new Vector3[]{p10,p12,p13,p14});

        }

        void Awake()
        {
            for (int i = 0; i < NonlinearTetra.Count; i++)
            {
                var te = NonlinearTetra[i];
                for (int k = 0; k < NonlinearTetra[i].Length; k++)
                {
                    var p0 =te[k];
                    print("p0 is" + p0);
                    print("what");
                    for (int j = 0; j < 4; j++)
                    {
                        var p1 = te[j];
                        print( p1);
                        if (j > k)
                        {
                           print(p0 +","+ p1);

                        }

                    }
                }
            }
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            //Gizmos.DrawCube(new Vector3(0,0,0),new Vector3(100,100,100));
            for (int i = 0; i < NonlinearTetra.Count; i++)
            {
                for (int k = 0; k < NonlinearTetra[i].Length; k++)
                {
                    var p0 = NonlinearTetra[i][k];

                    for (int j = 0; j < NonlinearTetra[i].Length; j++)
                    {
                        var p1 = NonlinearTetra[i][j];
                        if (j > k)
                        {
                            Gizmos.DrawLine(p0, p1);

                        }

                    }
                }
            }
        }


    }


}