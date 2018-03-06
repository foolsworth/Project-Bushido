using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionScript_v2 : MonoBehaviour {
    //The time since this object was created
    private float m_accumulatedTime;

    //necessary gameObjects
    public GameObject m_tunnel;
    public GameObject m_yourFace;

    //parameters
    public float m_waitTime = 5.0f; //This will be overridden in the editor if a different value is given.
    public float m_duration = 5.0f; //The number of seconds that the movement should take.

    //Calculation area
    private Vector3 m_source;
    private Vector3 m_destination;
    private Vector3 m_destinationOffset;

	// Use this for initialization
	void Start () {
        m_accumulatedTime = 0.0f;
        m_source = m_tunnel.transform.position; //I will need to copy the values (as there is no Vector3 copy constructor) if this is changing from frame to frame.
        m_destinationOffset = new Vector3(0.0f, 0.0f, (-0.75f * m_tunnel.transform.localScale.y));
        m_destination = new Vector3(m_yourFace.transform.position.x, m_yourFace.transform.position.y, m_yourFace.transform.position.z);
        m_destination += m_destinationOffset;
        //Debug.Log("Dstination is: " + m_destination);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Isn't it changing? \t " + m_source); //I want this not to be changing.
        //Debug.Log("Is it changing? \t " + m_destination);  //I want this to be changing.
    }

    private void FixedUpdate() {
        m_accumulatedTime += Time.fixedDeltaTime;
        m_destination = m_yourFace.transform.position + m_destinationOffset; //If I'm wrong about this being a pointer, I don't need this line.

        //if(m_accumulatedTime <= m_waitTime + m_duration) { //Disable movement if the action is finished.
        m_tunnel.transform.position = LerpPosition(m_source, m_destination, (m_accumulatedTime - m_waitTime) / m_duration);
        //Debug.Log("Current Position is: " + m_tunnel.transform.position);
        // } else {
        //    m_tunnel.transform.position = m_yourFace.transform.position;
        // }
    }

    private Vector3 LerpPosition(Vector3 source, Vector3 destination, float timeValue) {
        return new Vector3(Mathf.Lerp(source.x, destination.x, timeValue), Mathf.Lerp(source.y, destination.y, timeValue), Mathf.Lerp(source.z, destination.z, timeValue));
    }
}
