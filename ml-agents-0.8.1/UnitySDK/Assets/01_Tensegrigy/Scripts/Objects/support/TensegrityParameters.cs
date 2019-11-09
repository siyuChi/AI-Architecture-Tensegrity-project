using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZH.Tensegrity
{
    [Serializable]
	public class TensegrityParameters 
	{
        public float spring = 100f;

        public float damper = 50f;

		public float PointSize=0.1f;

		public float BarThickness=0.1f;

        public float StringThickness=0.05f;

		public float CenterSize=0.3f;

        [Range(0, 1f)] public float PreTenseParameter = 1f;

	}

}
