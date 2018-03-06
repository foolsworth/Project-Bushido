using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionScript : MonoBehaviour {
    //publicly accessible variables
    public float terminus = -75.0f;
    public float waitTime = 5.0f;
    public float duration = 5.0f;
    public Transform yourFace;

    //Make this the capsule
    public GameObject tunnel;

    float accumulatedTime;
    bool destinationReached;
    //Lerping variables
    Vector3 source;
    Vector3 destination;
    float lerpTime;
    float destinationTime;

	// Use this for initialization
	void Start () {
        //initialise variables
        accumulatedTime = 0;
        lerpTime = 0;
        destinationReached = false;
        source = tunnel.transform.position;
        destination = new Vector3(tunnel.transform.position.x, tunnel.transform.position.y, tunnel.transform.position.z + terminus);
        destinationTime = float.PositiveInfinity;
        if(!yourFace) {
            yourFace = Camera.main.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Nothing in the Update() method.
        Debug.Log(Camera.main.transform.position);
	}

    private void FixedUpdate() {
        //Make accumulatedTime accurate.
        accumulatedTime += Time.deltaTime;
        //update Lerping variables
        //lerpTime = ((destinationReached) ? (accumulatedTime - destinationTime) : ((accumulatedTime >= waitTime) ? (accumulatedTime - waitTime) : (0.0f)));
        lerpTime = ((destinationReached) ? ((accumulatedTime - waitTime) / duration) : ((accumulatedTime >= waitTime) ? ((accumulatedTime - waitTime - duration) / 1.0f) : (0.0f)));
        //update the Lerps
        tunnel.transform.position = new Vector3(Mathf.Lerp(source.x, destination.x, lerpTime * Time.deltaTime), Mathf.Lerp(source.y, destination.y, lerpTime * Time.deltaTime), Mathf.Lerp(source.z, destination.z, lerpTime * Time.deltaTime));
        //set destination
        if(Vector3.Distance(tunnel.transform.position, destination) <= 0.001f && !destinationReached) {
            destinationReached = true;
            source = tunnel.transform.position;
            destination = yourFace.position;
        }

        /*//check if waitTime is accurate
        if (accumulatedTime >= waitTime) {
            //check if we're there already
            if (tunnel.transform.position.z != terminus) {
                //linearly interpolate our current position from our current position to the goal position according to accumulatedTime after the wait.
                tunnel.transform.position = new Vector3(tunnel.transform.position.x, tunnel.transform.position.y, Mathf.Lerp(tunnel.transform.position.z, terminus, (accumulatedTime - waitTime) * Time.deltaTime));
            } else {
                //If we are, say so
                destinationReached = true;
            }
        }

        if(accumulatedTime >= waitTime && destinationReached) {

            }*/
    }

    private Vector3 LerpPosition(Vector3 source, Vector3 destination, float timeValue) {
        return new Vector3(Mathf.Lerp(source.x, destination.x, timeValue), Mathf.Lerp(source.y, destination.y, timeValue), Mathf.Lerp(source.z, destination.z, timeValue));
    }
}
