using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledTools {
	namespace VertexWindNamespace {

		//The main vertex wind script
		[AddComponentMenu("Untitled Tools/Vertex Wind/Vertex Wind Compute Script")]
		public class VertexWindCompute : MonoBehaviour {

			[Space(10f)]
			[Header("- Vertex Wind Settings -")]
			[Space(10f)]
			[Tooltip("The Compute Shader That Calculates The Wind")]
			public ComputeShader WindShader;
			[Tooltip("All Of The Objects That Wind Will Be Applied To")]
			public MeshFilter[] WindObjects;
			[Tooltip("The Speed Of The Wind")]
			public float Speed = 0.1f;
			[Tooltip("The Perlin Map Scale")]
			public float Scale = 2f;
			[Tooltip("Higher Values Lower Overall Bending")]
			public float Bending = 4f;
			[Tooltip("The Seed For The Random Perlin Map Starting Point")]
			public int StartingSeed = 1234;

			[Space(7f)]
			[Header("Advanced Features:")]
			[Space(2f)]
			[Tooltip("Only Works For Mesh Colliders!, \nUpdates The Mesh Collider For The Objects Dynamically. \nWARNING! This Is Extremely Tasking!")]
			public bool UpdateMeshCollider = false;
			[Tooltip("How Many Objects To Update Per Frame")]
			public int ObjectsPerFrame = -1;

			[Space(7f)]
			[Header("Directionality Settings:")]
			[Space(2f)]
			[Tooltip("How Much The Wind Will Bend On The X-Axis\n(Same For Percent Z)")]
			[Range(0f, 1f)]
			public float PercentX = 0.5f;
			[Tooltip("How Much The Wind Will Move On The Y-Axis")]
			public float YFactor = 0.2f;
			[HideInInspector]
			public float PercentZ;

			private Mesh[] DefaultMeshes;
			private Mesh GeneratedMesh;
			private float VertTime = 0f;
			private int d = 0;
			private float OriginalBending;
			private float OriginalSpeed;
			private float[] Offsets;

			private Vector3[] NewVerts;
			private int index = 0;
			private ComputeBuffer[] VertexBuffers;

			#region APIVars
			public Mesh[] ConvertedMeshes;
			#endregion

			//Initializes On Start
			public void Start () {

				//Checks to make sure compute shaders are supported
				if (!SystemInfo.supportsComputeShaders) {
					Debug.Log ("Compute shaders are not supported on this device!");
					enabled = false;
				}

				//Finds The Vertex Time Based On A Seed
				System.Random StartingRan = new System.Random (StartingSeed);
				VertTime = (float)StartingRan.Next (-50000, 50000);

				//Assigns The Default Meshes
				DefaultMeshes = new Mesh[WindObjects.Length];
				VertexBuffers = new ComputeBuffer[WindObjects.Length];
				Offsets = new float[WindObjects.Length];
				System.Random RanOffset = new System.Random ();
				for (int i = 0; i < DefaultMeshes.Length; i++) {
					DefaultMeshes [i] = WindObjects [i].sharedMesh;
					DefaultMeshes [i].MarkDynamic ();

					Offsets [i] = (float)RanOffset.Next (-100000, 100000) + (float)RanOffset.NextDouble ();
				}

				d = 0;
				index = 0;

				if (ObjectsPerFrame > DefaultMeshes.Length) {
					ObjectsPerFrame = DefaultMeshes.Length;
				}
				if (ObjectsPerFrame < 1) {
					ObjectsPerFrame = DefaultMeshes.Length;
				}
					
				OriginalBending = Bending;
				OriginalSpeed = Speed;

				foreach (MeshFilter m in WindObjects) {
					m.gameObject.isStatic = false;
				}

				//Sets default compute shader values
				WindShader.SetFloat ("Bending", Bending);
				WindShader.SetFloat ("Scale", Scale);
				WindShader.SetFloat ("PercentX", PercentX);
				WindShader.SetFloat ("PercentZ", PercentZ);
				WindShader.SetFloat ("YFactor", YFactor);

			}

			void Update () {

				//Checks to make sure compute shaders are supported
				if (!SystemInfo.supportsComputeShaders) {
					Debug.Log ("Compute shaders are not supported on this device!");
					enabled = false;
				}

				if (d < DefaultMeshes.Length) {

					for (int o = 0; o < ObjectsPerFrame; o++) {

						index = d + o;

						//Only runs If the scale is greater than zero
						if (Scale != 0f && Bending != 0f) {

							//Increases the vertex time
							VertTime += Speed; 
							GeneratedMesh = new Mesh ();
							GeneratedMesh.MarkDynamic ();

							//Finds the new vertex positions based on the default mesh and the vertex time
							NewVerts = new Vector3[DefaultMeshes [index].vertices.Length];

							int Kernel = WindShader.FindKernel ("CSMain");
							VertexBuffers [index] = new ComputeBuffer (DefaultMeshes [index].vertices.Length, 12, ComputeBufferType.Default);
							VertexBuffers [index].SetData (DefaultMeshes [index].vertices);
							WindShader.SetFloat ("TimeOffset", VertTime * Speed + Offsets [index]);
							WindShader.SetBuffer (Kernel, "VertexPositions", VertexBuffers [index]);

							WindShader.Dispatch (Kernel, NewVerts.Length, 1, 1);
							VertexBuffers [index].GetData (NewVerts);
							VertexBuffers [index].Dispose ();

							//Assigns the new vertex positions and all other default mesh properties
							GeneratedMesh.subMeshCount = DefaultMeshes [index].subMeshCount;
							GeneratedMesh.vertices = NewVerts;

							for (int t = 0; t < DefaultMeshes [index].subMeshCount; t++) {
								GeneratedMesh.SetTriangles (DefaultMeshes [index].GetTriangles (t), t);
							}

							//Sets original properties
							GeneratedMesh.uv = DefaultMeshes [index].uv;
							GeneratedMesh.uv2 = DefaultMeshes [index].uv2;
							GeneratedMesh.uv3 = DefaultMeshes [index].uv3;
							GeneratedMesh.uv4 = DefaultMeshes [index].uv4;
							GeneratedMesh.bindposes = DefaultMeshes [index].bindposes;
							GeneratedMesh.boneWeights = DefaultMeshes [index].boneWeights;
							GeneratedMesh.colors = DefaultMeshes [index].colors;
							GeneratedMesh.colors32 = DefaultMeshes [index].colors32;
							GeneratedMesh.tangents = DefaultMeshes [index].tangents;
							GeneratedMesh.normals = DefaultMeshes [index].normals;

							//Recalculates the mesh boundaries
							GeneratedMesh.RecalculateBounds ();

							//Assigns The New Mesh
							WindObjects [index].mesh = GeneratedMesh;

							if (UpdateMeshCollider && WindObjects [index].GetComponent<MeshCollider> ()) {
								WindObjects [index].GetComponent<MeshCollider> ().sharedMesh = GeneratedMesh;
							}

						}

					}

					d += ObjectsPerFrame;

				} else {
					d = 0;
				}

			}

			//Multiplies the power (bending and speed) by a set power factor
			public void MultiplyPower (float PowerMultiplier) {
				Bending = OriginalBending / PowerMultiplier;
				Speed = OriginalSpeed * PowerMultiplier;
			}

			//Returns the power of the wind to the original power
			public void OriginalPower () {
				Bending = OriginalBending;
				Speed = OriginalSpeed;
			}

		}
	}
}
