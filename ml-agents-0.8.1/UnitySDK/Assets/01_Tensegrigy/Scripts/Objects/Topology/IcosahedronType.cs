using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace ZH.Tensegrity
{
    public class IcosahedronType:TensegrityObject
    {
        [Header("CustomParam")]
        public float halfSize = 3f;

        public override void  SetupPoints()
        {
            Matrix4x4 matrixXZ = Matrix4x4.LookAt(Vector3.zero, Vector3.back, Vector3.down);
            Matrix4x4 matrixXY = Matrix4x4.LookAt(Vector3.zero, Vector3.down, Vector3.left);
            Matrix4x4 matrixYZ = Matrix4x4.LookAt(Vector3.zero, Vector3.left, Vector3.back);

           
            elements.ConstructionPoints.AddRange(TensegrityUtil.GetGoldenRectPoints(halfSize, matrixXZ));
            elements.ConstructionPoints.AddRange(TensegrityUtil.GetGoldenRectPoints(halfSize, matrixXY));
            elements.ConstructionPoints.AddRange(TensegrityUtil.GetGoldenRectPoints(halfSize, matrixYZ));
            int index = 0;
           for(int i=0;i<elements.ConstructionPoints.Count;i++)
            {
                elements.ConstructionPoints[i] = TensegrityUtil.CleanUp(elements.ConstructionPoints[i]);
                var p = elements.ConstructionPoints[i];
                var ep = Instantiate(prefabs.m_PointPrefab, transform);
                ep.transform.localPosition = p;
                ep.name = "endpoint_" + index;
                ep.index = index;
				if (ep.GetComponentInChildren<Text> ()) 
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
            int index = 0;
            for (int i = 0; i < elements.ConstructionPoints.Count; i += 2)
            {
                var start = elements.ConstructionPoints[i];
                var end = elements.ConstructionPoints[i + 1];
                var ep0 = elements.EndPoints[i];
                var ep1 = elements.EndPoints[i + 1];
                var bar = Instantiate(prefabs.m_BarPrefab, transform);
                bar.SetUp(ep0, ep1,param.BarThickness);

                bar.index = index;
                bar.name = $"Bar_{index}_s{ep0.index}_e{ep1.index}";
                elements.Bars.Add(bar);
                index++;
            }
        }

        public override void SetupStrings()
        {
            int index = 0;
            int toIndex = 0;
            for(int i = 0; i < elements.Bars.Count; i+=1)
            {
                if (i % 2 == 0)
                {
                    toIndex = i + 2 >= elements.Bars.Count ? 0 : i + 2;
                }
                var fromBar = elements.Bars[i];
                var toBar0 = elements.Bars[toIndex];
                var toBar1 = elements.Bars[toIndex + 1];

                NodePoint fromPoint0 = fromBar.startPoint;
                NodePoint fromPoint1 = fromBar.endPoint;

                NodePoint toPoint0;
                NodePoint toPoint1;

                if (i % 2 == 0)
                {
                    toPoint0 = toBar0.startPoint;
                    toPoint1 = toBar1.startPoint;
                }
                else
                {
                    toPoint0 = toBar0.endPoint;
                    toPoint1 = toBar1.endPoint;
                }

                var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                str0.SetUp(fromPoint0, toPoint0, param.StringThickness);
				str0.index = index++;
                str0.name = $"String_{str0.index}_s{fromPoint0.index}_e{toPoint0.index}";
             

                var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                str1.SetUp(fromPoint0, toPoint1, param.StringThickness);
				str1.index = index++;
                str1.name = $"String_{str1.index}_s{fromPoint0.index}_e{toPoint1.index}";

                var str2 = Instantiate(prefabs.m_StringPrefab, transform);
                str2.SetUp(fromPoint1, toPoint0, param.StringThickness);
				str2.index = index++;
				str2.name = $"String_{str2.index}_s{fromPoint1.index}_e{toPoint0.index}";

                var str3 = Instantiate(prefabs.m_StringPrefab, transform);
                str3.SetUp(fromPoint1, toPoint1, param.StringThickness);
				str3.index = index++;
				str3.name = $"String_{str3.index}_s{fromPoint1.index}_e{toPoint1.index}";

                str0.AddSymmetry(str2);
                str0.AddOpposite(str1);

                str3.AddSymmetry(str1);
                str3.AddOpposite(str2);

                elements.Strings.Add(str0);
                elements.Strings.Add(str1);
                elements.Strings.Add(str2);
                elements.Strings.Add(str3);
            }
        }


    }
}
