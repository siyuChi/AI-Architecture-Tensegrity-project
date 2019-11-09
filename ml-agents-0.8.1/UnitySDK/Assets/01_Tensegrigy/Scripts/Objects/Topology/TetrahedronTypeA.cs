using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZH.Tensegrity
{
    public class TetrahedronTypeA : TensegrityObject
    {

        public ObjectType myType = ObjectType.TetrahedronTypeA;
        const int pointCount = 4;
        [Header("CustomParam")]
        public Vector3[] tetraPoints;
        public float radius = 3f;
        [Range(0, 1f)] public float offsetParameter = 0.8f;


        public override void SetupPoints()
        {
            
            if (tetraPoints == null || tetraPoints.Length == 0)
            {
                tetraPoints = TensegrityUtil.GetTetraPoints(radius);
            }

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



        public override void SetupBars()
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

        public override void SetupStrings()
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





        public List<string> ConnectedUnits;

        public void ConnectToNext(TetrahedronTypeA next)
        {
            if (next == this)
            {
                return;
            }

            if (ConnectedUnits == null)
            {
                ConnectedUnits = new List<string>();
            }

            if (ConnectedUnits.Contains(next.name) || next.ConnectedUnits.Contains(this.name))
            {
                return;
            }

            ApplyPhysics();
            ToggleFreeze(true);

            next.ApplyPhysics();
            next.ToggleFreeze(true);


            foreach (var ep0 in elements.EndPoints)
            {
                foreach (var ep1 in next.elements.EndPoints)
                {
                    if (ep0.position == ep1.position)
                    {
                        var fromBar = ep0.AttatchedBar;
                        var toBar = ep1.AttatchedBar;

                        var m_ConfigurableJoint = fromBar.gameObject.AddComponent<ConfigurableJoint>();
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

                        if (!ConnectedUnits.Contains(next.name))
                        {
                            ConnectedUnits.Add(next.name);
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




        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }


}