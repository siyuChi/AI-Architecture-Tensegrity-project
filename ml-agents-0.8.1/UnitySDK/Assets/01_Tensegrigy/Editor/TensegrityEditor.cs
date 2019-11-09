using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ZH.Tensegrity.Behavior;


namespace ZH.Tensegrity
{
    [CustomEditor(typeof(TensegrityBehavior))]
	[CanEditMultipleObjects]
    public class TensegrityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();


			float width = 150f;
			float space = 3f;

            GUILayout.BeginHorizontal();
            var obj = (TensegrityBehavior)target;
			if(GUILayout.Button("Build",GUILayout.Width(width)))
            {
                if (!obj.my_Object.isBuilt)
                {
                    obj.my_Object.SetupStructure();
                }
                if (obj.buildWithPhysics)
                {
                    obj.my_Object.ApplyPhysics();
                }
            }
			GUILayout.Space (space);
			if (GUILayout.Button("Apply Physics",GUILayout.Width(width)))
            {
                if (obj.my_Object.isBuilt&&!obj.my_Object.physicsApplied)
                {
                    obj.my_Object.ApplyPhysics();
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
			if (GUILayout.Button("Clear",GUILayout.Width(150f)))
            {
                if (obj.my_Object.isBuilt)
                {
                    obj.my_Object.Clear();
                }
            }
			GUILayout.Space (space);
			if (GUILayout.Button("Reset",GUILayout.Width(width)))
            {
                if (obj.my_Object.isBuilt&&obj.my_Object.physicsApplied)
                {
                    obj.my_Object.ResetStructure();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
			if (GUILayout.Button("Freeze",GUILayout.Width(2f*(width+space))))
            {
                if (obj.my_Object.isBuilt && obj.my_Object.physicsApplied)
                {
                    obj.my_Object.ToggleFreeze(!obj.my_Object.isFreezed);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SetDefualt", GUILayout.Width(2f * (width + space))))
            {
                if (obj.my_Object.isBuilt && obj.my_Object.physicsApplied)
                {
                    obj.my_Object.SetAsDefualt();
                }
            }
            GUILayout.EndHorizontal();

         

            if (obj.my_Object.isBuilt && obj.my_Object.physicsApplied&&!Application.isPlaying)
            {
                foreach (var s in obj.my_Object.elements.Strings)
                {
                    if (s.limit.limit != s.defaultGeometryLength * obj.my_Object.param.PreTenseParameter)
                    {
                        s.SetLength(s.defaultGeometryLength * obj.my_Object.param.PreTenseParameter);
                    }
                }
            }
   
            
        }
    }
}

