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
    //public float m_bufferTime = 0.0f; //Uncomment lines 42 & 47 and optionally lines 45 & 46 in order fot this variable to have any meaning.

    //Calculation area
    private Vector3 m_source;
    private Vector3 m_destination;
    private Vector3 m_destinationOffset;

	// Use this for initialization
	void Start () {
        //instantiate zero time
        m_accumulatedTime = 0.0f;
        m_source = m_tunnel.transform.position; //This statement copies the values, this is the desired effect.
        //We need to test this in VR as the player's head position/rotation might not be accurate in this Start().  If this doesn't work, we'll have to do it on the first frame or something.
        //set the rotation of the tunnel so that the sphere is in front of the player.
        Vector3 l_desiredDirection = Vector3.ProjectOnPlane(m_yourFace.transform.forward, Vector3.up);
        float l_angle = Vector3.SignedAngle(Vector3.forward, l_desiredDirection, Vector3.up);
        Quaternion l_rotation = Quaternion.Euler(0.0f, l_angle, 0.0f);
        m_tunnel.transform.rotation = l_rotation * m_tunnel.transform.rotation; //This might be easier to do with either Quaternion.SetFromToRotation(m_tunnel.transform.forward, l_desiredDirection) or Vector3.RotateTowards(m_tunnel.transform.forward, l_desiredDirection, 2 * Mathf.PI, 0.0f);
        //Set the offset from the origin desired so that the sphere lands on the player.
        //m_destinationOffset = new Vector3(0.0f, 0.0f, (-0.75f * m_tunnel.transform.localScale.y)); //This was accurate when the player always started pointing down -z, but with player rotation, this is wrong.
        m_destinationOffset = m_tunnel.GetComponentInChildren<Transform>().position * -1.0f;
        m_destination = new Vector3(m_yourFace.transform.position.x, m_yourFace.transform.position.y, m_yourFace.transform.position.z);
        m_destination += m_destinationOffset;
        //Debug.Log("Dstination is: " + m_destination);
        Debug.Log("Sphere.position is actually: " + (m_tunnel.GetComponentInChildren<Transform>().localPosition + m_tunnel.transform.position));
	}

	void Update () {
        //Debug.Log("Isn't it changing? \t " + m_source); //I want this not to be changing.
        //Debug.Log("Is it changing? \t " + m_destination);  //I want this to be changing.
    }

    private void FixedUpdate() {
        m_accumulatedTime += Time.fixedDeltaTime;
        m_destination = m_yourFace.transform.position + m_destinationOffset; //If I'm wrong about this being a pointer, I don't need this line.

        //if(m_accumulatedTime <= m_waitTime + m_duration + m_bufferTime) { //Disable movement if the action is finished.
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
