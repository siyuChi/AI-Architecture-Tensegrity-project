using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
	public abstract class TensileElement : MonoBehaviour,ITensegrityElements
	{
        #region Public Propories
        public Vector3 Start;
        public Vector3 End;
        public Vector3 defaultPosition;
        public Quaternion defaultRotation;
        public Vector3 defaultObjectScale;
		public int index;
        public float defaultGeometryLength;
        public float defualtLimitLength;
        public float length;
       	public  SoftJointLimit limit;
        public SoftJointLimitSpring spring;
		public GameObject m_stringObject;
		public NodePoint startPoint;
		public NodePoint endPoint;
        public TensileElement symmetry;
        public TensileElement opposite;
        public ConfigurableJoint m_ConfigurableJoint;
        #endregion

        #region Public Methods
        public void AddSymmetry(TensileElement sym)
        {
            if (symmetry != null && symmetry == sym)
            {
                return;
            }
            symmetry = sym;
            sym.AddSymmetry(this);
        }

        public void AddOpposite(TensileElement oppo)
        {
            if (opposite != null && opposite == oppo)
            {
                return;
            }
            opposite = oppo;
            oppo.AddOpposite(this);
        }

        public void ToPhysics(TensegrityParameters param)
        {
			if (m_ConfigurableJoint != null) 
			{
				return;
			}
            var fromBar = startPoint.AttatchedBar;
            var toBar = endPoint.AttatchedBar;
            m_ConfigurableJoint = fromBar.gameObject.AddComponent<ConfigurableJoint>();
            m_ConfigurableJoint.connectedBody = toBar.body;
            m_ConfigurableJoint.autoConfigureConnectedAnchor = false;
            m_ConfigurableJoint.enableCollision = true;
            m_ConfigurableJoint.anchor = startPoint.transform.localPosition;
            m_ConfigurableJoint.connectedAnchor = endPoint.transform.localPosition;
            m_ConfigurableJoint.xMotion = ConfigurableJointMotion.Limited;
            m_ConfigurableJoint.yMotion = ConfigurableJointMotion.Limited;
            m_ConfigurableJoint.zMotion = ConfigurableJointMotion.Limited;
            m_ConfigurableJoint.angularXMotion = ConfigurableJointMotion.Free;
            m_ConfigurableJoint.angularYMotion = ConfigurableJointMotion.Free;
            m_ConfigurableJoint.angularZMotion = ConfigurableJointMotion.Free;
             limit = m_ConfigurableJoint.linearLimit;
             spring = m_ConfigurableJoint.linearLimitSpring;
            defualtLimitLength= defaultGeometryLength * param.PreTenseParameter;
            limit.limit = defualtLimitLength;
            spring.spring = param.spring;
            spring.damper = param.damper;
            m_ConfigurableJoint.linearLimit = limit;
            m_ConfigurableJoint.linearLimitSpring = spring;
        }
        #endregion

        #region Abstract Methods
        public abstract void SetUp(Vector3 start, Vector3 end, float thickness = 0.1f);
        public abstract void SetUp(NodePoint start, NodePoint end, float thickness = 0.1f);
        public abstract void SetLength(float targetLength);

        public abstract void SetLengthByParameter(float parameter);
        public abstract void ResetLength();
        public abstract void UpdateElement();
        public abstract void ResetString();
        public abstract void SetDefualt();

        #endregion
	    public abstract void lostphysics();
	    public abstract void GetCurlength(float _length);
	    public abstract void SetCurLengthByParameter(float curlength, float parameter);

        public abstract NodePoint StartPt { get; }
        public abstract NodePoint EndPt { get; }
	}
}
