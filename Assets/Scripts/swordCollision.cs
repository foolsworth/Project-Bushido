using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollision : MonoBehaviour {

    public AudioSource swordhit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "swordCollider")
        {
            swordhit.Play();
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
