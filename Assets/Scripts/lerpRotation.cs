using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpRotation : MonoBehaviour {

    public Transform hand;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation != hand.rotation)
        {
            transform.rotation = hand.rotation;
        }
    }
}
