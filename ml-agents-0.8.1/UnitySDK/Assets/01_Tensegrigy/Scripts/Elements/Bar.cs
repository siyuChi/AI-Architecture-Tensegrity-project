using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
	public class Bar : RigidElement 
	{
		public override void SetUp (Vector3 start, Vector3 end, float thickness = 0.1f)
		{
			Start = start;
			End = end;
			var d = end - start;
            defaultLength = d.magnitude;

            defaultPosition = 0.5f * d + Start;
            defaultRotation = Quaternion.LookRotation(d);
            defaultObjectScale = new Vector3(thickness, thickness, defaultLength);

            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            m_BarObject.transform.localScale = defaultObjectScale;
        }

        public override void SetUp(NodePoint start, NodePoint end, float thickness = 0.1f)
        {
            Start = start.position;
            End = end.position;
            startPoint = start;
            endPoint = end;
            var d = End - Start;



            defaultLength = d.magnitude;

            defaultPosition = 0.5f * d + Start;
            defaultRotation = Quaternion.LookRotation(d);
            defaultObjectScale = new Vector3(thickness, thickness, defaultLength);

            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            m_BarObject.transform.localScale = defaultObjectScale;

			start.AttatchToBar (this, NodePoint.EndPointType.IsStart);
			end.AttatchToBar (this, NodePoint.EndPointType.IsEnd);
        }

        public override void ResetLength ()
		{
			throw new System.NotImplementedException ();
		}

		public override void SetLength (float targetLength)
		{
			throw new System.NotImplementedException ();
		}
        public override void ResetBar()
        {
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
        }
        public override void SetDefualt()
        {
             defaultPosition= transform.position;
            defaultRotation = transform.rotation;
        }
    }

}

