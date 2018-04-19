using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour {

    public OurPlayerManager manager;

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 10 && c.collider.transform.parent.parent.gameObject.layer == 11)
        {

            manager.takeDamage(10);

        }
    }
            // Use this for initialization
            void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
