﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour {

    OurPlayerManager manager;
    bool collided = false;

    public int damage;
    public AudioClip sfx;
    public GameObject me;
    GameObject spawnPoint1;

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer == 11 && !collided)
        {
            collided = true;
            manager.takeDamage(damage);
            //Vector3 dir = GameObject.FindGameObjectWithTag("handE").transform.position - me.transform.position;
            // We then get the opposite (-Vector3) and normalize it
            // dir = -dir.normalized;
            // GameObject.Find("GhostBody").transform.Find("Controller (left)").GetComponent<Dashing>().Knockback(dir);
            me.GetComponent<AudioSource>().Stop();
            me.GetComponent<AudioSource>().clip = sfx;
            me.GetComponent<AudioSource>().Play();
        }
    }
    private void OnCollisionExit(Collision c)
    {
        collided = false;
    }
    
        // Use this for initialization
        void Start () {
        manager = GameObject.FindGameObjectWithTag("local").GetComponent<OurPlayerManager>();
        spawnPoint1 = GameObject.FindGameObjectWithTag("spawnInArena");

    }

        void Update()
    {
        if (manager.health==0)
        {
            OurPlayerManager.dead=true;
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
                child.gameObject.AddComponent<Sliceable>();

                }
                StartCoroutine(reset());
            
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

        armor.transform.position = spawnPoint1.transform.position;
        armor.transform.rotation = spawnPoint1.transform.rotation;
        GameObject.Find("HeightChecker").transform.position = spawnPoint1.transform.position;
        GameObject.Find("GhostBody").transform.position = armor.transform.position;
        GameObject.Find("GhostBody").transform.rotation = armor.transform.rotation;
    }
}
