using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollision : MonoBehaviour {

    public AudioSource swordhit;
    public GameObject rightH;

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 10)
        {
            Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;

            float force = 10;
            swordhit.Stop();
            swordhit.Play();
            SteamVR_Controller.Input(1).TriggerHapticPulse(2000);
            SteamVR_Controller.Input(2).TriggerHapticPulse(2000);
            //transform.parent.GetComponent<Rigidbody>().velocity += c.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            //GetComponent<Rigidbody>().AddForce(dir * force);
            GetComponent<Rigidbody>().AddForceAtPosition(dir * force, c.contacts[0].point);
            rightH.GetComponent<SpringJoint>().spring = 0;
            rightH.GetComponent<SpringJoint>().damper = 0;
            rightH.GetComponent<SpringJoint>().tolerance = Mathf.Infinity;
            rightH.GetComponent<SpringJoint>().maxDistance = Mathf.Infinity;


            //GetComponent<Rigidbody>().drag = 100;

        }
        //if (collision.collider.gameObject.layer == 13)
        //{
        //    Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;
        //    collision.collider.transform.parent.GetComponent<OurPlayerManager>().takeDamage(10);
        //    swordhit.Stop();
        //    swordhit.Play();
        //    transform.parent.GetComponent<Rigidbody>().velocity += collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
        //    collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity += myVel;
        //}
    }


    private void OnCollisionExit(Collision c)
    {

            rightH.GetComponent<SpringJoint>().spring = 1000;
            rightH.GetComponent<SpringJoint>().damper = 20;
            rightH.GetComponent<SpringJoint>().tolerance = 1e-20F;
            rightH.GetComponent<SpringJoint>().maxDistance = 0;
        //GetComponent<Rigidbody>().drag = 10 ;

    }
        // Use this for initialization
        void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
