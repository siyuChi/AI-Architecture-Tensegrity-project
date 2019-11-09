using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
	public class NodePoint : MonoBehaviour 
	{
        public RigidElement AttatchedBar;

        public EndPointType type;

        public int index;

		public Vector3 position{ get; set;}

		public void AttatchToBar(RigidElement bar,EndPointType pointType)
		{
			AttatchedBar = bar;
			transform.parent = bar.transform;
            transform.localPosition = TensegrityUtil.CleanUp(transform.localPosition);
            type = pointType;

            switch (pointType)
            {
                case EndPointType.IsStart:
                  
                    transform.localRotation = Quaternion.Euler(-90f, 0, 0);
                    bar.startPoint = this;
                    break;
                case EndPointType.IsEnd:
                    transform.localRotation = Quaternion.Euler(90f, 0, 0);
                    bar.endPoint = this;
                    break;
            }

		}

		public void UpdatePosition()
		{
			position = transform.position;
		}

		public enum EndPointType
		{
			IsStart,IsEnd
		}
	}


}

