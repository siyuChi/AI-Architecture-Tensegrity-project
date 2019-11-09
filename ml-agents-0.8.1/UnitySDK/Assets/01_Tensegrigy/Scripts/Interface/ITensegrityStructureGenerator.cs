using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZH.Tensegrity
{
	public interface ITensegrityStructureGenerator 
	{
		void SetupPoints ();

		void SetupBars ();

		void SetupStrings ();

		void ApplyPhysics ();
	}
}

