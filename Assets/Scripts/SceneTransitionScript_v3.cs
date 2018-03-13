using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionScript_v3 : MonoBehaviour {

    float m_accumulatedTime;

    GameObject m_tunnelParent;
    public GameObject m_player;

    //parameters  //These will be overridden in the editor if a different value is given.
    public float m_waitTime = 5.0f;
    public float m_animationDuration = 5.0f;

    private Vector3 m_source;
    private Vector3 m_destination;

	// Use this for initialization
	void Start () {
        m_accumulatedTime = 0.0f;
        m_tunnelParent = gameObject;
        m_tunnelParent.transform.position = Vector3.Normalize(Vector3.ProjectOnPlane(m_player.transform.forward, Vector3.up)) * 150.0f;

        m_source = m_tunnelParent.transform.position; //Desired is a copy of present values.
        m_destination = m_player.transform.position;

        //set the rotation of the prop/tunnel so that the sphere/"opening" is in front of the player
        Vector3 l_desiredDirection = Vector3.ProjectOnPlane(m_player.transform.forward, Vector3.up);
        float l_angle = Vector3.SignedAngle(Vector3.forward, l_desiredDirection, Vector3.up);
        //Quaternion l_rotation = Quaternion.Euler(0.0f, l_angle, 0.0f); //This is not needed in this version

        m_tunnelParent.transform.RotateAround(m_destination, Vector3.up, l_angle);
	}

    private void FixedUpdate() {
        m_accumulatedTime += Time.fixedDeltaTime;
        m_destination = m_player.transform.position;

        m_tunnelParent.transform.position = Vector3.Lerp(m_source, m_destination, (m_accumulatedTime - m_waitTime) / m_animationDuration);
    }
}
