using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpBody : MonoBehaviour {

    public Transform head;
    public int updateSpeed = 20;
	// Use this for initialization
	void Start () {
       // head =gameObject.transform.parent.Find("head").transform.Find("Neck").transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position != head.position)
        {
            transform.position =Vector3.Lerp(transform.position, head.position, Time.deltaTime*updateSpeed);
        }
        if (transform.rotation != head.rotation)
        {
            Vector3 headrotation = head.rotation.eulerAngles;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, headrotation.y, 0), Time.deltaTime * updateSpeed);
        }
    }
}
