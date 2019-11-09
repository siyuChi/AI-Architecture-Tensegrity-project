using System;
using UnityEngine;

namespace ZH.Tensegrity
{
    public class NonlinearConnect : TensegrityObject
    {
        [SerializeField] TetrahedronTypeNonliner _type;
        [Header("ConnectBar")] public NodePoint[] BarPoints = new NodePoint[8];
        [Header("ConnectString")]
        public NodePoint[] StringPointsFrom = new NodePoint[12];
        public NodePoint[] StringPointsTo = new NodePoint[24];

        public override void SetupPoints()
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

            StringPointsFrom[3] = _type.elements.EndPoints[15];
            StringPointsFrom[4] = _type.elements.EndPoints[16];
            StringPointsFrom[5] = _type.elements.EndPoints[17];

            StringPointsFrom[6] = _type.elements.EndPoints[45];
            StringPointsFrom[7] = _type.elements.EndPoints[46];
            StringPointsFrom[8] = _type.elements.EndPoints[47];

            StringPointsFrom[9] = _type.elements.EndPoints[39];
            StringPointsFrom[10] = _type.elements.EndPoints[40];
            StringPointsFrom[11] = _type.elements.EndPoints[41];

            StringPointsTo[0] = _type.elements.EndPoints[11];
            StringPointsTo[1] = _type.elements.EndPoints[8];
            StringPointsTo[2] = _type.elements.EndPoints[10];
            StringPointsTo[3] = _type.elements.EndPoints[5];
            StringPointsTo[4] = _type.elements.EndPoints[7];
            StringPointsTo[5] = _type.elements.EndPoints[3];

            StringPointsTo[6] = _type.elements.EndPoints[25];
            StringPointsTo[7] = _type.elements.EndPoints[30];
            StringPointsTo[8] = _type.elements.EndPoints[27];
            StringPointsTo[9] = _type.elements.EndPoints[31];
            StringPointsTo[10] = _type.elements.EndPoints[28];
            StringPointsTo[11] = _type.elements.EndPoints[24];

            StringPointsTo[12] = _type.elements.EndPoints[35];
            StringPointsTo[13] = _type.elements.EndPoints[32];
            StringPointsTo[14] = _type.elements.EndPoints[29];
            StringPointsTo[15] = _type.elements.EndPoints[34];
            StringPointsTo[16] = _type.elements.EndPoints[27];
            StringPointsTo[17] = _type.elements.EndPoints[31];

            StringPointsTo[18] = _type.elements.EndPoints[49];
            StringPointsTo[19] = _type.elements.EndPoints[54];
            StringPointsTo[20] = _type.elements.EndPoints[55];
            StringPointsTo[21] = _type.elements.EndPoints[51];
            StringPointsTo[22] = _type.elements.EndPoints[48];
            StringPointsTo[23] = _type.elements.EndPoints[52];
        }

        public override void SetupBars()
        {
            int index = 0;
            for (int n = 0; n < 8; n += 2)
            {
                var b0 = Instantiate(prefabs.m_BarPrefab, transform);
                b0.SetUp(BarPoints[n], BarPoints[n + 1], param.BarThickness);
                b0.index = index++;
                b0.name = $"Bar_{b0.index}_s{BarPoints[n].index}_e{BarPoints[n + 1].index}";
                print(b0.name);
            }
        }

        public override void SetupStrings()
        {
            int index = 0;
            for (int n = 0; n < 12; n++)
            {
                var str0 = Instantiate(prefabs.m_StringPrefab, transform);
                str0.SetUp(StringPointsFrom[n], StringPointsTo[2 * n], param.StringThickness);
                str0.index = index++;
                str0.name = $"String_{str0.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * n].index}";
                print(str0.name);

                var str1 = Instantiate(prefabs.m_StringPrefab, transform);
                str1.SetUp(StringPointsFrom[n], StringPointsTo[2 * n + 1], param.StringThickness);
                str1.index = index++;
                str1.name = $"String_{str1.index}_s{StringPointsFrom[n].index}_e{StringPointsTo[2 * n + 1].index}";
                print(str1.name);
            }
        }
    }
}