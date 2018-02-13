using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cantMoveIfYouTouchMe : MonoBehaviour {
    private bool triggered = false;
    private List< Collider> collidedObj = new List<Collider>();
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        collidedObj.Add( other);
        triggered = true;
        
    }
    void OnTriggerExit(Collider other)
    {
        collidedObj.Remove(other);
    }

        // Update is called once per frame
        void FixedUpdate () {
        if (triggered)
        {
            foreach(Collider c in collidedObj)
            {
                Debug.Log("this should be working");
                //c.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //c.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //c.transform.GetComponent<Rigidbody>().Sleep();
                //c.transform.GetComponent<Rigidbody>().mass = 50;
                //c.attachedRigidbody.angularVelocity = Vector3.zero;
                c.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            
        }
	}
}
