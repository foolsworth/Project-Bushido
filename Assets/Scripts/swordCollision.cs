﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollision : MonoBehaviour {

    public AudioSource swordhit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 11)
        {
            Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;
            

            swordhit.Stop();
            swordhit.Play();
            transform.parent.GetComponent<Rigidbody>().velocity += collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
            collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity += myVel;

        }
        if (collision.collider.gameObject.layer == 13)
        {
            Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;
            collision.collider.transform.parent.GetComponent<OurPlayerManager>().takeDamage(10);
            swordhit.Stop();
            swordhit.Play();
            transform.parent.GetComponent<Rigidbody>().velocity += collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
            collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity += myVel;
        }
    }

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
