using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ZH.Tensegrity
{
    public class PhysicalModelComplete : TensegrityObject
    {

        private List<Vector3[]> NonlinearTetra = new List<Vector3[]>();
        [Range(0, 10)] public float moveBar = 0;

        const int pointCount = 4;
        [Header("CustomParam")]
        public float radius = 3f;
        private float offsetParameterA = 0.75f;
        private float offsetParameterB = 0.71f;

        [Header("ConnectString")]
        public NodePoint[] StringPointsFrom = new NodePoint[6];
        public NodePoint[] StringPointsTo = new NodePoint[6];



        public override void SetupPoints()
        {
            SetTetraFrame();
            for (int n = 0; n < NonlinearTetra.Count; n++)
            {
                if (n == 0)
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
                        elements.ConstructionPoints.Add(offsetParameterA * d1 + p1);
                        elements.ConstructionPoints.Add(offsetParameterA * d2 + p2);
                        elements.ConstructionPoints.Add(offsetParameterA * d3 + p3);
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
                if (n == 1)
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
                        elements.ConstructionPoints.Add((1f - offsetParameterB) * d1 + cen);
                        elements.ConstructionPoints.Add((1f - offsetParameterB) * d2 + cen);
                        elements.ConstructionPoints.Add((1f - offsetParameterB) * d3 + cen);
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

            #region connect

            StringPointsFrom[0] = elements.EndPoints[21];
            StringPointsFrom[1] = elements.EndPoints[22];
            StringPointsFrom[2] = elements.EndPoints[23];

            StringPointsFrom[3] = elements.EndPoints[14];//8,11
            StringPointsFrom[4] = elements.EndPoints[17];//5,10
            StringPointsFrom[5] = elements.EndPoints[20];//7,3


            for (int i = 0; i < 6; i++)
            {
                elements.EndPoints.Add(StringPointsFrom[i]);
            }

            StringPointsTo[0] = elements.EndPoints[11];
            StringPointsTo[1] = elements.EndPoints[8];
            StringPointsTo[2] = elements.EndPoints[10];
            StringPointsTo[3] = elements.EndPoints[5];
            StringPointsTo[4] = elements.EndPoints[7];
            StringPointsTo[5] = elements.EndPoints[3];
            for (int i = 0; i < 6; i++)
            {
                elements.EndPoints.Add(StringPointsTo[i]);
            }
            #endregion
        }

        public override void SetupBars()
        {
            for (int n = 0; n < NonlinearTetra.Count; n++)
            {
                if (n == 0 || n == 1)
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
                if (n == 0 || n == 1)
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
            for (int n = 0; n < 3; n++)
            {
                var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                str0.SetUp(StringPointsFrom[n], StringPointsTo[2 * n], param.StringThickness);
                str0.index = index1++;
                str0.name = $"CoString_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * n].index}";
                print(str0.name);
                elements.Strings.Add(str0);

                var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                str1.SetUp(StringPointsFrom[n], StringPointsTo[2 * n + 1], param.StringThickness);
                str1.index = index1++;
                str1.name = $"CoString_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * n + 1].index}";
                print(str1.name);
                elements.Strings.Add(str1);
            }
            for (int n = 3; n < 6; n++)
            {
                var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                str0.SetUp(StringPointsFrom[n], StringPointsTo[2 * (n - 3)], param.StringThickness);
                str0.index = index1++;
                str0.name = $"CoString_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * (n - 3)].index}";
                print(str0.name);
                elements.Strings.Add(str0);

                var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                str1.SetUp(StringPointsFrom[n], StringPointsTo[2 * (n - 3) + 1], param.StringThickness);
                str1.index = index1++;
                str1.name = $"CoString_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * (n - 3) + 1].index}";
                print(str1.name);
                elements.Strings.Add(str1);
            }
            #endregion
        }

        public void SetTetraFrame()
        {
            Vector3 p1 = new Vector3(0, 0, 0);
            Vector3 p2 = new Vector3(758.946f, 1.339f, 0);
            Vector3 p3 = new Vector3(378.313f, 657.936f, 0);
            Vector3 p4 = new Vector3(379.1f, 219.8f, 619.7f);
            Vector3 p5 = new Vector3(1010.9f, 586f, 413.118f);

            NonlinearTetra.Add(new Vector3[] { p1, p2, p3, p4 });
            NonlinearTetra.Add(new Vector3[] { p2, p3, p4, p5 });
        }



    }
}

