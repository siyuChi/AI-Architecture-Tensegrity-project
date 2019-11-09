using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ZH.Tensegrity
{
	public class String : TensileElement 
	{
        #region Public Methods
        public override void SetUp (Vector3 start, Vector3 end, float thickness = 0.1f)
		{
            Start = start;
            End = end;
            var d = end - start;
            defaultGeometryLength = d.magnitude;
            length = defualtLimitLength;

            defaultPosition = 0.5f * d + Start;
            defaultRotation = Quaternion.LookRotation(d);
            defaultObjectScale = new Vector3(thickness, thickness, defaultGeometryLength);

            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            m_stringObject.transform.localScale = defaultObjectScale;
        }

        public override void SetUp(NodePoint start, NodePoint end, float thickness = 0.1f)
        {
            Start = start.position;
            End = end.position;
            startPoint = start;
            endPoint = end;
            var d = End - Start;
            defaultGeometryLength = d.magnitude;
            length = defualtLimitLength;

            defaultPosition = 0.5f * d + Start;
            defaultRotation = Quaternion.LookRotation(d);
            defaultObjectScale= new Vector3(thickness, thickness, defaultGeometryLength);

            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            m_stringObject.transform.localScale = defaultObjectScale;
        }

        public override void SetLength (float targetLength)
		{
		    if (m_ConfigurableJoint != null)
		    {
		        limit.limit = targetLength;
		        m_ConfigurableJoint.linearLimit = limit;
            }
           
		}

        public override void SetLengthByParameter(float parameter)
        {
            float length = defualtLimitLength * parameter;
            SetLength(length);
        }


        public override void ResetLength ()
		{
            SetLength(defualtLimitLength);
		}

        public override void SetDefualt()
        {
            defualtLimitLength = length;
            defaultGeometryLength = length;

           defaultPosition= transform.position ;
            defaultRotation = transform.rotation;
            defaultObjectScale = m_stringObject.transform.localScale;

        }

        public override void UpdateElement ()
		{
            var d = endPoint.position - startPoint.position;
            transform.position = 0.5f * d + startPoint.position;
            transform.rotation = Quaternion.LookRotation(d);
            length = d.magnitude;
           var s= m_stringObject.transform.localScale;
            s.z = length;
            m_stringObject.transform.localScale = s;
        }

        public override void ResetString()
        {
            ResetLength();
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            m_stringObject.transform.localScale = defaultObjectScale;

        }
        #endregion

	    public override void lostphysics()
	    {
	        SoftJointLimitSpring _spr = new SoftJointLimitSpring();
	        _spr.spring = 10f;
	        _spr.damper = 10f;
	        m_ConfigurableJoint.linearLimitSpring = _spr;

	    }

	    public override void GetCurlength(float _length )
	    {
	        _length = length;
	    }


	    public override void SetCurLengthByParameter(float curlength, float parameter)
	    {
	        float le = curlength + parameter;
	        if (le > 0)
	        {
	            SetLength(le);
	        }
	    }

	    public override NodePoint StartPt { get { return startPoint;} }
	    public override NodePoint EndPt { get { return endPoint;} }

    }
}