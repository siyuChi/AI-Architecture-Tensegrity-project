using UnityEngine;
using System.Collections;

namespace ZH.Tensegrity.Behavior
{
    public class TensegrityBehavior : MonoBehaviour
    {
        public bool buildWithPhysics = false;
        public TensegrityObject my_Object { get { return GetComponent<TensegrityObject>(); } }

    }
}

