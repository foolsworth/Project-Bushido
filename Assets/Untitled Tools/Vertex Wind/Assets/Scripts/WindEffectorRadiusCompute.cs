using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools {
	namespace VertexWindNamespace {

		//A script that can effect other vertex wind scripts in a given radius
		[AddComponentMenu("Untitled Tools/Vertex Wind/Wind Effector Radius Compute")]
		public class WindEffectorRadiusCompute : MonoBehaviour {

			//The settings for the wind effector radius script
			[Space(10f)]
			[Header("- Wind Compute Radius Effector Settings -")]
			[Space(10f)]
			[Tooltip("The radius of that in which other wind script will be affected")]
			public float Radius = 10f;
			[Tooltip("Does the radius use the local scale of the object?")]
			public bool UseLocalScale = false;
			[Tooltip("What speed and bending will be multiplied by in the set radius")]
			public float PowerMultiplier = 2f;

			//The settings for visualization of the wind effector in the editor
			[Space(7f)]
			[Header("Visualizer Settings")]
			[Tooltip("How big the vertex wind script points are in the editor")]
			public float ScriptPointSize = 1f;

			private VertexWindCompute[] VertexWindScripts;
			private List<VertexWindCompute> AvailableScripts = new List<VertexWindCompute> ();
			private List<VertexWindCompute> OutsideScripts = new List<VertexWindCompute> ();
				
			//Updates every frame
			void Update () {

				//Uses the local scale to determine the radius of the effector
				if (UseLocalScale) {
					Radius = transform.localScale.x * transform.localScale.y * transform.localScale.z;
				}

				//Figures out the scripts inside and outside of the effector's radius
				VertexWindScripts = VertexWindCompute.FindObjectsOfType<VertexWindCompute> ();
				AvailableScripts.Clear ();
				OutsideScripts.Clear ();
				for (int i = 0; i < VertexWindScripts.Length; i++) {
					float DistanceFromEffector = Vector3.Distance (VertexWindScripts [i].transform.position, transform.position);
					if (DistanceFromEffector <= Mathf.Abs (Radius)) {
						AvailableScripts.Add (VertexWindScripts [i]);
					} else {
						OutsideScripts.Add (VertexWindScripts [i]);
					}
				}

				//Multiplies the power of all scripts in the radius by a power factor
				for (int i = 0; i < AvailableScripts.Count; i++) {
					AvailableScripts [i].MultiplyPower (PowerMultiplier);
				}

				//Returns all scripts outside of the radius to their original power
				for (int i = 0; i < OutsideScripts.Count; i++) {
					OutsideScripts [i].OriginalPower ();
				}

			}

			//Draws the gizmos for visualization of the effector
			void OnDrawGizmos () {

				//Checks if the wind effector is enabled
				if (enabled) {

					//Uses the local scale to determine the radius of the effector
					if (UseLocalScale) {
						Radius = transform.localScale.x * transform.localScale.y * transform.localScale.z;
					}

					//Sets the gizmos color and draws a sphere around the effector
					Gizmos.color = Color.blue;
					Gizmos.DrawWireSphere (transform.position, Radius);

					//Figures out the scripts inside and outside of the effector's radius
					VertexWindScripts = VertexWindCompute.FindObjectsOfType<VertexWindCompute> ();
					AvailableScripts.Clear ();
					OutsideScripts.Clear ();
					for (int i = 0; i < VertexWindScripts.Length; i++) {
						float DistanceFromEffector = Vector3.Distance (VertexWindScripts [i].transform.position, transform.position);
						if (DistanceFromEffector <= Mathf.Abs (Radius)) {
							AvailableScripts.Add (VertexWindScripts [i]);
						} else {
							OutsideScripts.Add (VertexWindScripts [i]);
						}
					}

					//Draws the points to visualize where the vertex wind scripts are
					Gizmos.color = Color.red;
					for (int i = 0; i < AvailableScripts.Count; i++) {
						Gizmos.DrawWireSphere (AvailableScripts [i].transform.position, ScriptPointSize);
					}

				}

			}

		}

	}
}
