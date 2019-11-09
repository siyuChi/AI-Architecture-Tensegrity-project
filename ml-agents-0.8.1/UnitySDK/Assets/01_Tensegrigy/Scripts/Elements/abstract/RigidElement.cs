using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
	public abstract class RigidElement : MonoBehaviour,ITensegrityElements
	{
        #region Public Properties
        public Vector3 Start;
        public Vector3 End;
        public int index;
        public float defaultLength;
        public Vector3 defaultPosition;
        public Quaternion defaultRotation;
        public Vector3 defaultObjectScale;
		public NodePoint startPoint;
		public NodePoint endPoint;
		public GameObject m_BarObject;
        public Rigidbody body;
        #endregion

        #region Abstract Methods
        public abstract void SetUp (Vector3 start, Vector3 end, float thickness = 0.1f);
        public abstract void SetUp(NodePoint start, NodePoint end, float thickness = 0.1f);
        public abstract void SetLength (float targetLength);
		public abstract void ResetLength ();
        public abstract void ResetBar();
        public abstract void SetDefualt();

        #endregion

        #region Public Methods
        public void ToPhysics(TensegrityParameters param)
        {
            if (!GetComponent<Rigidbody>())
            {
                body=gameObject.AddComponent<Rigidbody>();

                body.isKinematic = true;
            }
        }
        public void ToggleKinematic(bool toggle)
        {
            body.isKinematic = toggle;
        }
        #endregion
    }

}

