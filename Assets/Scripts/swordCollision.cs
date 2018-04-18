using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollision : MonoBehaviour {

    public AudioSource swordhit;

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 10)
        {
            Vector3 myVel = transform.parent.GetComponent<Rigidbody>().velocity;

            float force = 100;
            swordhit.Stop();
            swordhit.Play();
            //transform.parent.GetComponent<Rigidbody>().velocity += c.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);

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

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
