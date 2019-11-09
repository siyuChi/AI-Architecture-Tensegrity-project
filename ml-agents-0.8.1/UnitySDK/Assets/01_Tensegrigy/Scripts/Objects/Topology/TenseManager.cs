using UnityEngine;
using System.Collections.Generic;

namespace ZH.Tensegrity
{
    public class TenseManager:MonoBehaviour
    {

        //change position of joint
        [Range(0, 1f)] public float manualController = 1f;
        [SerializeField] private TetrahedronType tetraM;


        private void Move()
        {
            for (int i = 0; i < tetraM.elements.ConstructionPoints.Count; i++)
            {
              tetraM.elements.ConstructionPoints[i]+=Vector3.one*manualController;
            }
        }


        void Update()
        {
            if (tetraM.elements.ConstructionPoints.Count!=0)
            {
                tetraM.elements.ConstructionPoints[0] += Vector3.one * manualController;

            }
        }
    }
}

