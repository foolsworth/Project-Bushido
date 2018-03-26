using System.Collections;
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
            try
            {
                transform.parent.GetComponent<Rigidbody>().velocity += collision.collider.gameObject.transform.parent.GetComponent<Rigidbody>().velocity;
                collision.collider.gameObject.transform.parent.parent.GetComponent<Rigidbody>().velocity += myVel;
            }
            catch { }
            collision.collider.gameObject.transform.parent.parent.parent.GetComponent<OurPlayerManager>().takeDamage(10);
        }
    }

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
