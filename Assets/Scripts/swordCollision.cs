﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollision : MonoBehaviour {

    public AudioSource swordhit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 10)
        {
            Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;
            

            swordhit.Stop();
            swordhit.Play();
            transform.parent.GetComponent<Rigidbody>().velocity += collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
            collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity += myVel;

        }
    }

    // Use this for initialization
    void Start () {
        swordhit = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
