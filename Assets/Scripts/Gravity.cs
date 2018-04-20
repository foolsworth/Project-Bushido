﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    public GameObject spawnPoint1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 10, 0), Vector3.down, Color.green);
        if (Physics.Raycast(transform.position+new Vector3(0,10,0), Vector3.down, out hit))
        {
            if (hit.transform.tag=="dangerNet" && GameObject.FindGameObjectWithTag("local") != null)
            {
                GameObject armor = GameObject.FindGameObjectWithTag("local");
                foreach (Transform child in armor.transform)
                {
                    if (child.name == "head" || child.name == "body" || child.name == "right" || child.name == "left")
                    {
                        if (child.name == "body")
                        {
                            child.GetComponent<lerpBody>().enabled = false;
                        }
                        child.GetComponent<Rigidbody>().isKinematic = false;
                        child.GetComponent<Rigidbody>().useGravity = true;
                        //child.GetComponent<Collider>().enabled = true;
                        
                    }
                    child.tag = "localArmorChild";
                    child.transform.parent = null;
                    OurPlayerManager.dead = true; 
                }
                StartCoroutine(reset());
            }
        }

    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(3);
        gameObject.transform.position = spawnPoint1.transform.position;
        gameObject.transform.rotation = spawnPoint1.transform.rotation;

        GameObject armor = GameObject.FindGameObjectWithTag("local");
        GameObject[] children = GameObject.FindGameObjectsWithTag("localArmorChild");
        foreach (GameObject child in children)
        {
        if (child.name == "head" || child.name == "body" || child.name == "right" || child.name == "left")
        {
                if (child.name == "body")
                {
                    child.GetComponent<lerpBody>().enabled = true;
                }
                child.GetComponent<Rigidbody>().isKinematic = true;
            child.GetComponent<Rigidbody>().useGravity = false;
        }
        //    if(child.name!="left"|| child.name != "right")
        //    {
        //        child.GetComponent<Collider>().enabled = false;
        //    }
           
            child.transform.parent = armor.transform;
            OurPlayerManager.dead = false;
        }

        armor.transform.position = gameObject.transform.position;
        armor.transform.rotation = gameObject.transform.rotation;
        GameObject.Find("HeightChecker").transform.position = gameObject.transform.position;
    }
}
