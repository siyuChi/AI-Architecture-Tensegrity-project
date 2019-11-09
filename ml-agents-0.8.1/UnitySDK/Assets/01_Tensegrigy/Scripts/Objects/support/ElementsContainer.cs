using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZH.Tensegrity.Support
{
   [Serializable]
    public class ElementsContainer
    {
        public List<Vector3> ConstructionPoints;
        public List<NodePoint> EndPoints;
        public List<RigidElement> Bars;
        public List<TensileElement> Strings;
        public TensegrityCenter center;

        public void Initialize()
        {
            ConstructionPoints = new List<Vector3>();
            EndPoints = new List<NodePoint>();
            Bars = new List<RigidElement>();
            Strings = new List<TensileElement>();
        }
        public void Clear()
        {
            if (ConstructionPoints != null)
            {
                ConstructionPoints.Clear();
            }

            if (EndPoints != null)
            {
                foreach (var p in EndPoints)
                {
                    UnityEngine.Object.DestroyImmediate(p.gameObject);
                }
                EndPoints.Clear();
            }

            if (Bars != null)
            {
                foreach (var b in Bars)
                {
                    UnityEngine.Object.DestroyImmediate(b.gameObject);
                }
                Bars.Clear();
            }

            if (Strings != null)
            {
                foreach (var s in Strings)
                {
                    UnityEngine.Object.DestroyImmediate(s.gameObject);
                }
                Strings.Clear();
            }

            if (center != null)
            {
                UnityEngine.Object.DestroyImmediate(center.gameObject);
                center = null;
            }
        }
    }

    [Serializable]
    public class PrefabContainer
    {
        public TensileElement m_StringPrefab;
        public RigidElement m_BarPrefab;
        public NodePoint m_PointPrefab;
        public TensegrityCenter m_Center;
    }
    [Serializable]
    public abstract class CustomParameters
    {

    }
}
