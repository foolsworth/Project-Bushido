using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpBody : MonoBehaviour {

    public Transform head;
    public int updateSpeed = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition != head.localPosition)
        {
            transform.localPosition =Vector3.Lerp(transform.localPosition, head.localPosition, Time.deltaTime*updateSpeed);
        }
        if (transform.localRotation != head.localRotation)
        {
            Vector3 headrotation = head.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, headrotation.y, 0), Time.deltaTime * updateSpeed);
        }
    }
}
