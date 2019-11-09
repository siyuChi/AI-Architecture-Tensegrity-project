using UnityEngine;
using System.Collections;
using ZH.Tensegrity;

namespace ZH.Tensegrity.Behavior
{
    public class TensegrityPlayer : MonoBehaviour
    {
        TensegrityObject my_Object { get { return GetComponent<TensegrityObject>(); } }

        void FixedUpdate()
        {
            if (my_Object.isBuilt && !my_Object.isFreezed) //my_Object.physicsApplied && 
            {
                my_Object.UpdateStructure();
            }

        }
    }
}
