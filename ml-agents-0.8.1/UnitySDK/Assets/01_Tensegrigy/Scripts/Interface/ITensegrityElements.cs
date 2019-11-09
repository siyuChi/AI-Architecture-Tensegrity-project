using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
	public interface ITensegrityElements  
	{

		void SetUp (Vector3 start, Vector3 end, float thickness = 0.1f);

        void SetUp(NodePoint start, NodePoint end, float thickness = 0.1f);

        void SetLength (float targetLength);

		void ResetLength ();

        void ToPhysics(TensegrityParameters param);
	}
}

