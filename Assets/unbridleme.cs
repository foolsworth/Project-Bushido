using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unbridleme : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.GetComponent<Rigidbody>().mass = gameObject.transform.GetComponent<Rigidbody>().mass/4;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
