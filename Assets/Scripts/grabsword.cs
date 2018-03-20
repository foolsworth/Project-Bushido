using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabsword : MonoBehaviour {
    public Transform targetTransform;
    public GameObject openHand;
    public GameObject closedHand;
    public GameObject openGHand;
    public GameObject closedGHand;
    public AudioSource unsheath;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("sword collision");
        if ( collision.gameObject.tag == "Sword")
        {
            closedHand.SetActive(true);
            openHand.SetActive(false);
         if(closedGHand!=null && openGHand != null)
            {
                closedGHand.SetActive(true);
                openGHand.SetActive(false);
                unsheath.Play();
            }
            
            
            collision.gameObject.transform.parent = gameObject.transform;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.transform.position = targetTransform.position;
            collision.gameObject.transform.rotation = targetTransform.rotation;
            collision.gameObject.transform.localScale = targetTransform.localScale;
            this.GetComponent<FixedJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }

        // Use this for initialization
        void Start () {
        GameObject Ghost = GameObject.Find("GhostBody").gameObject;
        GameObject GhostHan = Ghost.transform.Find("Controller (right)").gameObject;
        closedGHand = GhostHan.transform.Find("Tsujigiri_FirstHero_HandGuantlet_RIGHT_Closed (1)").gameObject;

        openGHand = GhostHan.transform.Find("Tsujigiri_FirstHero_HandGauntlet_RIGHT").gameObject;
        unsheath = GhostHan.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
