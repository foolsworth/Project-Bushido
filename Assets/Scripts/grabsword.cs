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
    public bool swordgrabbed = false;
    public GameObject sword;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("sword collision");
        if ( collision.gameObject.tag == "Sword" && !GameObject.FindGameObjectWithTag("local").GetComponent<OurPlayerManager>().unsheathed)
        {
            closedHand.SetActive(true);
            openHand.SetActive(false);
         if(closedGHand!=null && openGHand != null)
            {
                closedGHand.SetActive(true);
                openGHand.SetActive(false);
                unsheath.Play();
            }
            
            
            collision.gameObject.GetComponent<Collider>().enabled = false;
            //collision.gameObject.GetComponent<MeshCollider>().enabled = true;
            collision.gameObject.transform.position = targetTransform.position;
            collision.gameObject.transform.rotation = targetTransform.rotation;
            collision.gameObject.transform.localScale = targetTransform.localScale;
            collision.gameObject.transform.parent = this.transform;
            GameObject.FindGameObjectWithTag("local").GetComponent<OurPlayerManager>().unsheathed = true;
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
        if (GameObject.FindGameObjectWithTag("local").GetComponent<OurPlayerManager>().unsheathed && sword.transform.position != targetTransform.position)
        {
            closedHand.SetActive(true);
            openHand.SetActive(false);
            if (closedGHand != null && openGHand != null)
            {
                closedGHand.SetActive(true);
                openGHand.SetActive(false);
                unsheath.Play();
                gameObject.GetComponent<swordCollision>().enabled = true;
            }


            sword.GetComponent<Collider>().enabled = false;
            //sword.GetComponent<MeshCollider>().enabled = true;
            sword.transform.position = targetTransform.position;
            sword.transform.rotation = targetTransform.rotation;
            sword.transform.localScale = targetTransform.localScale;
            sword.transform.parent = this.transform;
        }
    }
}
