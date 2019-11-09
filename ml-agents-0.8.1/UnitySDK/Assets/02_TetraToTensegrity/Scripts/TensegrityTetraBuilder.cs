using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Unity.TetrahedralGrowth;
using ZH.Tensegrity;

public class TensegrityTetraBuilder : MonoBehaviour 
{
	[SerializeField] GrowthManager _manager;
	[SerializeField] TetrahedronType _type;
    
	List<TetrahedronType> structures;

	bool isFreezing=true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.B)) 
		{
			BuildTensegrity ();
		}
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			ConnectStructure ();
		}
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			isFreezing = !isFreezing;
			ToggleFreeze (isFreezing);
		}
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ResetAll ();
		}
	}

	public void BuildTensegrity()
	{
		if (structures == null) 
		{
			structures = new List<TetrahedronType> ();
		}
        
		var tetrahedrons = _manager.GetTetrahedron ();
		var verts = _manager.GetMesh ().Vertices;

		if (tetrahedrons.Count == structures.Count) 
		{
			return;
		}

		int startfrom = structures.Count;

		var tetra = tetrahedrons [startfrom];

		var p0 = (Vector3)verts [tetra.Vertex0].Position;
		var p1 = (Vector3)verts [tetra.Vertex1].Position;
		var p2 = (Vector3)verts [tetra.Vertex2].Position;
		var p3 = (Vector3)verts [tetra.Vertex3].Position;

		p0 = transform.TransformPoint (p0);
		p1 = transform.TransformPoint (p1);
		p2 = transform.TransformPoint (p2);
		p3 = transform.TransformPoint (p3);


		var cen = (p0 + p1 + p2 + p3) / 4f;
		var tetratype = Instantiate (_type, transform);
		tetratype.transform.position = cen;

		p0 = tetratype.transform.InverseTransformPoint (p0);
		p1 = tetratype.transform.InverseTransformPoint (p1);
		p2 = tetratype.transform.InverseTransformPoint (p2);
		p3 = tetratype.transform.InverseTransformPoint (p3);


		tetratype.SetupTetraPoints (new Vector3[]{ p0, p1, p2, p3 });
		tetratype.SetupStructure ();
		structures.Add (tetratype);
		tetratype.name = "tetra_" + startfrom;
	}

	public void ConnectStructure()
	{
		if (structures.Count == 1) 
		{
			structures [0].ApplyPhysics ();
			structures [0].ToggleFreeze (true);

		}
		foreach (var t in structures) 
		{
			foreach (var t1 in structures) 
			{
				t.ConnectToNext (t1);
			}
		}
	}

	public void ToggleFreeze(bool toggle)
	{
		foreach (var t in structures) 
		{
			t.ToggleFreeze (toggle);
		}
	}

	public void ResetAll()
	{
		foreach (var t in structures) 
		{
			t.ToggleFreeze (true);
			t.ResetStructure ();
			isFreezing = true;
		}
	}
}
