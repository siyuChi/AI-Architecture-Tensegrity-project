using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace ZH.Tensegrity
{
    public class TetrahedronType : TensegrityObject
    {
        public ObjectType mytype = ObjectType.TetrahedronTypeA;
        const int pointCount = 4;
        [Header("CustomParam")]
		public Vector3[] tetraPoints;
        public float radius = 3f;
        [Range(0,1f)]public float offsetParameter = 1f;

        [Header("control bar")]
        [Range(-2f, 2f)] public float[] manualController=new float[12];

        private List<Face> faces;

        public override void SetupBars()
        {
			SetBarsByType();
        }

        public override void SetupPoints()
        {
			if (tetraPoints == null || tetraPoints.Length == 0) 
			{
				tetraPoints = TensegrityUtil.GetTetraPoints(radius);
			}

            /// <summary>
            ///  choose the type!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /// </summary>
            /// <param name="v0"></param>
            /// <param name="v1"></param>
            /// <param name="v2"></param>
            /// <returns></returns>
            SetPointsByType();
            int index = 0;
            for (int i = 0; i < elements.ConstructionPoints.Count; i++)
            {
                elements.ConstructionPoints[i] = TensegrityUtil.CleanUp(elements.ConstructionPoints[i]);
                var p = elements.ConstructionPoints[i];
                var ep = Instantiate(prefabs.m_PointPrefab, transform);
                ep.transform.localPosition = p;
                ep.transform.localScale = Vector3.one * param.PointSize;
                ep.name = "endpoint_" + index;
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

        public override void SetupStrings()
        {
          SetStringsByType();
        }
        /// <summary>
        /// how to choose the type!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        void SetPointsByType()
        {
            if (mytype == ObjectType.TetrahedronTypeA)
            {
                for (int i = 0; i < tetraPoints.Length; i++)
                {
                    int to = i;
                    int from1 = i + 1 < tetraPoints.Length - 1 ? i + 1 : 0;
                    int from2 = from1 + 1 < tetraPoints.Length - 1 ? from1 + 1 : 0;
                    int from3 = i < tetraPoints.Length - 1 ? 3 : from2 + 1;
                    var p0 = tetraPoints[to];
                    var p1 = tetraPoints[from1];
                    var p2 = tetraPoints[from2];
                    var p3 = tetraPoints[from3];

                    var d1 = p0 - p1;
                    var d2 = p0 - p2;
                    var d3 = p0 - p3;
                    elements.ConstructionPoints.Add(offsetParameter * d1 + p1);
                    elements.ConstructionPoints.Add(offsetParameter * d2 + p2);
                    elements.ConstructionPoints.Add(offsetParameter * d3 + p3);
                }
            }
            if (mytype == ObjectType.TetrahedronTypeB)
            {
                for (int i = 0; i < tetraPoints.Length; i++)
                {
                    int to = i;
                    int from1 = i + 1 < tetraPoints.Length - 1 ? i + 1 : 0;
                    int from2 = from1 + 1 < tetraPoints.Length - 1 ? from1 + 1 : 0;
                    int from3 = i < tetraPoints.Length - 1 ? 3 : from2 + 1;
                    var p0 = tetraPoints[to];
                    var p1 = tetraPoints[from1];
                    var p2 = tetraPoints[from2];
                    var p3 = tetraPoints[from3];

                    var cen = (p1 + p2 + p3) / 3;

                    var d1 = cen - p1;
                    var d2 = cen - p2;
                    var d3 = cen - p3;
                    elements.ConstructionPoints.Add((1f-offsetParameter) * d1 + cen);
                    elements.ConstructionPoints.Add((1f-offsetParameter) * d2 + cen);
                    elements.ConstructionPoints.Add((1f - offsetParameter) * d3 + cen);
                }
            }
            if (mytype == ObjectType.TetrahedronTypeC)
            {
                Vector3[] PointsContainer = new Vector3[8];
                var t0 = tetraPoints[0];
                var t1 = tetraPoints[1];
                var t2 = tetraPoints[2];
                var t3 = tetraPoints[3];
                //print(tetraPoints.Length);
                var p4 = new Vector3();
                var p5 = new Vector3();
                var p6 = new Vector3();
                var p7 = new Vector3();
                for (int i = 0; i < tetraPoints.Length; i++)
                {

                    PointsContainer[0] = (t0 - t1 + t2 - t1) * 0.5f * 0.25f + t1;
                    PointsContainer[1] = (t1 - t0 + t3 - t0) * 0.5f * 0.25f + t0;
                    PointsContainer[2] = (t0 - t3 + t2 - t3) * 0.5f * 0.25f + t3;
                    PointsContainer[3] = (t1 - t2 + t3 - t2) * 0.5f * 0.25f + t2;

                    p4 = (t1 - t2 + t3 - t2) * 0.5f * 0.75f + t2;
                    p5 = (t1 - t0 + t3 - t0) * 0.5f * 0.75f + t0;
                    p6 = (t0 - t1 + t2 - t1) * 0.5f * 0.75f + t1;
                    p7 = (t0 - t3 + t2 - t3) * 0.5f * 0.75f + t3;

                    PointsContainer[4] = p4 + (p4 - p6) * 0.5f;
                    PointsContainer[5] = p5 + (p5 - p7) * 0.5f;
                    PointsContainer[6] = p6 + (p6 - p4) * 0.5f;
                    PointsContainer[7] = p7 + (p7 - p5) * 0.5f;
                }

                for (int i = 0; i < PointsContainer.Length; i++)
                {
                    elements.ConstructionPoints.Add(PointsContainer[i]);
                }
            }


            if (mytype == ObjectType.TetrahedronTypeB || mytype == ObjectType.TetrahedronTypeA)
            {
                if (manualController.Length != 0)
                {
                    for (int i = 0; i < elements.ConstructionPoints.Count; i++)
                    {
                        elements.ConstructionPoints[i] += Vector3.back * manualController[i];

                    }
                }
            }



        }
        void SetBarsByType()
        {
            if (mytype == ObjectType.TetrahedronTypeA||mytype==ObjectType.TetrahedronTypeB)
            {
                int size = 3;
                int index = 0;
                var top = elements.EndPoints.GetRange(elements.ConstructionPoints.Count - 3, 3);

                for (int i = 0, p = 0; i < tetraPoints.Length - 1; i++, p += 3)
                {
                    int topIndex = i + 1 < 3 ? i + 1 : 0;
                    int to = p - 3 < 0 ? elements.ConstructionPoints.Count - 6 : p - 3;

                    var toGroup = elements.EndPoints.GetRange(to, size);

                    var fromGroup = elements.EndPoints.GetRange(p, size);
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
            if (mytype == ObjectType.TetrahedronTypeC)
            {
                var bar0 = Instantiate(prefabs.m_BarPrefab, transform);
                bar0.SetUp(elements.EndPoints[0], elements.EndPoints[2], param.BarThickness);
                bar0.index = 0;
                bar0.name = $"Bar_{bar0.index}_s{elements.EndPoints[0].index}_e{elements.EndPoints[2].index}";
                elements.Bars.Add(bar0);

                var bar1 = Instantiate(prefabs.m_BarPrefab, transform);
                bar1.SetUp(elements.EndPoints[1], elements.EndPoints[3], param.BarThickness);
                bar1.index = 1;
                bar1.name = $"Bar_{bar1.index}_s{elements.EndPoints[1].index}_e{elements.EndPoints[3].index}";
                elements.Bars.Add(bar1);

                var bar2 = Instantiate(prefabs.m_BarPrefab, transform);
                bar2.SetUp(elements.EndPoints[4], elements.EndPoints[6], param.BarThickness);
                bar2.index = 2;
                bar2.name = $"Bar_{bar2.index}_s{elements.EndPoints[4].index}_e{elements.EndPoints[6].index}";
                elements.Bars.Add(bar2);

                var bar3 = Instantiate(prefabs.m_BarPrefab, transform);
                bar3.SetUp(elements.EndPoints[5], elements.EndPoints[7], param.BarThickness);
                bar3.index = 3;
                bar3.name = $"Bar_{bar3.index}_s{elements.EndPoints[5].index}_e{elements.EndPoints[7].index}";
                elements.Bars.Add(bar3);
            }
        }
        void SetStringsByType()
        {
            if (mytype == ObjectType.TetrahedronTypeA || mytype == ObjectType.TetrahedronTypeB)
            {
                int index = 0;
                int topIndex = 0;
                var top = elements.EndPoints.GetRange(elements.ConstructionPoints.Count - 3, 3);
                for (int i = 0; i < elements.ConstructionPoints.Count; i += 3)
                {
                    var corner = elements.EndPoints.GetRange(i, 3);

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

                    if (i < elements.EndPoints.Count - 3)
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
                        int toPt2 = i + 4 < elements.EndPoints.Count - 3 ? i + 4 : 1;
                        var ep4 = corner[fromPt2];
                        var ep5 = elements.EndPoints[toPt2];

                        var str2 = Instantiate(prefabs.m_StringPrefab, transform);
                        str2.SetUp(ep4, ep5, param.StringThickness);
                        str2.index = index++;
                        str2.name = $"String_{str2.index}_s{ep4.index}_e{ep5.index}";
                        elements.Strings.Add(str2);
                    }

                }
            }
            if (mytype == ObjectType.TetrahedronTypeC)
            {
                int index = 0;

                List<NodePoint> tempPoints = new List<NodePoint>();
                tempPoints.Add(elements.EndPoints[0]);
                tempPoints.Add(elements.EndPoints[6]);
                tempPoints.Add(elements.EndPoints[5]);
                tempPoints.Add(elements.EndPoints[7]);
                tempPoints.Add(elements.EndPoints[2]);
                foreach (var tp in tempPoints)
                {
                    for (int i = 1; i < 4; i += 2)
                    {
                        var str = Instantiate(prefabs.m_StringPrefab, transform);
                        str.SetUp(elements.EndPoints[i], tp, param.StringThickness);
                        str.index = index++;
                        str.name = $"String_{str.index}_s{elements.EndPoints[i].index}_e{tp.index}";
                        elements.Strings.Add(str);
                    }
                }

                for (int i = 4; i < 7; i++)
                {
                    var str = Instantiate(prefabs.m_StringPrefab, transform);
                    str.SetUp(elements.EndPoints[i], elements.EndPoints[0], param.StringThickness);
                    str.index = index++;
                    str.name = $"String_{str.index}_s{elements.EndPoints[i].index}_e{elements.EndPoints[0].index}";
                    elements.Strings.Add(str);
                }

                for (int i = 4; i < 6; i++)
                {
                    var str = Instantiate(prefabs.m_StringPrefab, transform);
                    str.SetUp(elements.EndPoints[i], elements.EndPoints[2], param.StringThickness);
                    str.index = index++;
                    str.name = $"String_{str.index}_s{elements.EndPoints[i].index}_e{elements.EndPoints[2].index}";
                    elements.Strings.Add(str);
                }
                var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                str1.SetUp(elements.EndPoints[7], elements.EndPoints[2], param.StringThickness);
                str1.index = index;
                str1.name = $"String_{str1.index}_s{elements.EndPoints[7].index}_e{elements.EndPoints[2].index}";
                elements.Strings.Add(str1);
            }
        }

        public List<string> ConnectedUnits;

		public void ConnectToNext(TetrahedronType next)
		{
			if (next == this) 
			{
				return;
			}

			if (ConnectedUnits == null) 
			{
				ConnectedUnits = new List<string> ();
			}

			if (ConnectedUnits.Contains (next.name) || next.ConnectedUnits.Contains (this.name)) 
			{
				return;
			}

			ApplyPhysics ();
			ToggleFreeze (true);

			next.ApplyPhysics ();
			next.ToggleFreeze (true);


			foreach (var ep0 in elements.EndPoints) 
			{
				foreach (var ep1 in next.elements.EndPoints) 
				{
					if (ep0.position == ep1.position) {
						var fromBar = ep0.AttatchedBar;
						var toBar = ep1.AttatchedBar;

						var m_ConfigurableJoint = fromBar.gameObject.AddComponent<ConfigurableJoint> ();
						m_ConfigurableJoint.connectedBody = toBar.body;
						m_ConfigurableJoint.autoConfigureConnectedAnchor = false;
						m_ConfigurableJoint.anchor = ep0.transform.localPosition;
						m_ConfigurableJoint.connectedAnchor = ep1.transform.localPosition;
						m_ConfigurableJoint.xMotion = ConfigurableJointMotion.Locked;
						m_ConfigurableJoint.yMotion = ConfigurableJointMotion.Locked;
						m_ConfigurableJoint.zMotion = ConfigurableJointMotion.Locked;
						m_ConfigurableJoint.angularXMotion = ConfigurableJointMotion.Free;
						m_ConfigurableJoint.angularYMotion = ConfigurableJointMotion.Free;
						m_ConfigurableJoint.angularZMotion = ConfigurableJointMotion.Free;
						var limit = m_ConfigurableJoint.linearLimit;
						var spring = m_ConfigurableJoint.linearLimitSpring;
						limit.limit = 0;
						spring.spring = 1000f;
						spring.damper = 300f;
						m_ConfigurableJoint.linearLimit = limit;
						m_ConfigurableJoint.linearLimitSpring = spring;

						if (!ConnectedUnits.Contains (next.name)) 
						{
							ConnectedUnits.Add (next.name);
						}
					} 
					else 
					{
						continue;
					}
				}
			}
		}

		public void SetupTetraPoints(Vector3[] tetra)
		{
			tetraPoints = tetra;
		}

        public void SetFace()
        {
            faces=new List<Face>();

            Face f0=new Face();

           f0.AddPoint(new int[]{0,2,9,10,5,4});
            faces.Add(f0);
        }


        /*
        void OnDrawGizmos()
        {

            Gizmos.color = Color.black;
            for (int i = 0; i < tetraPoints.Length; i++)
            {
                var p0 = transform.TransformPoint(tetraPoints[i]);
                Gizmos.DrawSphere(p0, 0.1f);

                for (int j = 0; j < tetraPoints.Length; j++)
                {

                    if (i == j)
                    {
                        continue;
                    }
                    var p1 = transform.TransformPoint(tetraPoints[j]);
                    Gizmos.DrawLine(p0, p1);
                }
            }
        }
        */
    }
}

