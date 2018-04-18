using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionScript_v3 : MonoBehaviour {

    float m_accumulatedTime;
    bool m_doesAccumulate;

    GameObject m_tunnelParent;
    public GameObject m_player;
    public GameObject[] m_otherRotators;

    //parameters  //These will be overridden in the editor if a different value is given.
    public float m_waitTime = 5.0f;
    public float m_animationDuration = 5.0f;
    public bool m_isDone { get; private set; }

    //doesn't change after being set
    private Vector3 m_source;

    //changes on fixedUpdate
    private Vector3 m_destination;

    private void Awake() {
        Debug.Log("Transition awake ...");
    }

    void Start () {
        Debug.Log("Transition started...?");
        m_doesAccumulate = true;
        m_isDone = false;
        m_accumulatedTime = 0.0f;
        m_tunnelParent = gameObject;
        m_tunnelParent.transform.position = Vector3.Normalize(Vector3.ProjectOnPlane(m_player.transform.forward, Vector3.up)) * 150.0f;

        m_source = m_tunnelParent.transform.position; //Desired is a copy of present values.
        m_destination = m_player.transform.position;
        
        //Set up the variables we need to rotate the tunnel
        Vector3 l_desiredDirection = Vector3.ProjectOnPlane(m_player.transform.forward, Vector3.up);
        float l_angle = Vector3.SignedAngle(Vector3.forward, l_desiredDirection, Vector3.up);
        //Quaternion l_rotation = Quaternion.Euler(0.0f, l_angle, 0.0f); //This is not needed in this version
        //set the rotation of the prop/tunnel so that the sphere/"opening" is in front of the player
        m_tunnelParent.transform.RotateAround(m_destination, Vector3.up, l_angle);
        foreach (GameObject rotator in m_otherRotators)
            rotator.transform.RotateAround(m_destination, Vector3.up, l_angle);
	}

    private void FixedUpdate() {
        if(m_doesAccumulate)
            m_accumulatedTime += Time.fixedDeltaTime;
        //Debug.Log(m_accumulatedTime);
        m_destination = m_player.transform.position;

        m_tunnelParent.transform.position = Vector3.Lerp(m_source, m_destination, (m_accumulatedTime - m_waitTime) / m_animationDuration);
        
        //This had been wrong, we were testing m_source, which doesn't change, with m_destination, which doesn't change much.  This iteration tests the correct things.
        m_isDone = (Vector3.Distance(m_tunnelParent.transform.position, m_destination) <= 1.0e-4f); //This number could probably be larger, but it isn't usually the source of the problems.  Make it bigger if you're debugging.

    }

    public void PauseTransition(bool unpause = false) {
        m_doesAccumulate = unpause;
        Debug.Log("Transition has " + (unpause? "un" : "") + "paused.");
    }
}
