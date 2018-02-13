using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpGhostHands : MonoBehaviour {

    public Transform hand;
    public int updateSpeed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition != hand.localPosition)
        {
            transform.localPosition =Vector3.Lerp(transform.localPosition, hand.localPosition, Time.deltaTime*updateSpeed);
        }
        if (transform.localRotation != hand.localRotation)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, hand.localRotation, Time.deltaTime*updateSpeed);
        }
    }
}
