﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour {

    OurPlayerManager manager;
    bool collided = false;

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 11 && !collided)
        {

            manager.takeDamage(10);
            Vector3 dir = c.collider.gameObject.transform.parent.parent.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            GameObject.Find("GhostBody").transform.Find("Controller (left)").GetComponent<Dashing>().Knockback(dir);

        }
    }
            // Use this for initialization
            void Start () {
        manager = gameObject.transform.parent.GetComponent<OurPlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
