using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZH.Tensegrity.Topology
{
	public class Polygon
	{
		public Polygon(Vector3[] pointset,int self,int[] adjacent ,float offset)
		{
			vertices = new Dictionary<int, Vertex> ();
			offset = Mathf.Clamp (offset,0f,1f);

			Vector3 myPoint = pointset [self];
			int index = 0;
			foreach (var vi in adjacent) 
			{
				Vector3 adjPoint = pointset [vi];

				Vector3 point = (adjPoint - myPoint) * offset + myPoint;

				var vex = new Vertex (point,self,vi);

				//vertices.Sort ();
			}
		}


		public Vertex center;

		public Dictionary<int,Vertex> vertices;


	}

	public class Triangle
	{
		public Vertex center;

		public Vertex A;
		public Vertex B;
		public Vertex C;

	}

	public class Vertex
	{
		public int end{ get; set;}
		public int start{ get; set;}

		public int index{ get; set;}
		public Vector3 position;

		public Vertex(Vector3 _position,int _start,int _end)
		{
			position = _position;
		}
	}
}

