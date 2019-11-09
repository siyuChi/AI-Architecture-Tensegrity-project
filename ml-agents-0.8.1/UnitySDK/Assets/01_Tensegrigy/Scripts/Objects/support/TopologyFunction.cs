using System;
using UnityEngine;

namespace ZH.Tensegrity
{
	public abstract class TopologyFunction:ScriptableObject
	{
        public abstract Vector3[] GetPoints();
	
	}
}

