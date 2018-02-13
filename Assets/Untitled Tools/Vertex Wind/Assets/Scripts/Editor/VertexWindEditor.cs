using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UntitledTools {
	namespace VertexWindNamespace {

		//Custom Editor For The Vertex Wind Script
		#if UNITY_EDITOR
		[CustomEditor(typeof(VertexWind))]
		public class VertexWindEditor : Editor {

			public override void OnInspectorGUI () {

				//Finds The Vertex Wind Script
				DrawDefaultInspector ();
				VertexWind TargetScript = (VertexWind)target;

				//Calculates The Percent That The Wind Should Bend On The Z-Axis
				float PercentZ = 1f - TargetScript.PercentX;
				GUILayout.Label ("Percent Z:  " + PercentZ);
				TargetScript.PercentZ = PercentZ;

				GUILayout.Space (15f);
				if (GUILayout.Button ("Use Children As Wind Objects", GUILayout.Height (20f))) {
					TargetScript.WindObjects = TargetScript.GetComponentsInChildren<MeshFilter> ();
				}
				if (GUILayout.Button ("Use This As The Wind Object", GUILayout.Height (20f))) {
					TargetScript.WindObjects = TargetScript.GetComponents<MeshFilter> ();
				}
				GUILayout.Space (15f);


			}

		}
		#endif

	}
}
