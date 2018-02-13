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
        if (collision.gameObject.layer == 9 && collision.gameObject.tag == "Sword")
        {
            closedHand.SetActive(true);
            openHand.SetActive(false);
            closedGHand.SetActive(true);
            openGHand.SetActive(false);
            unsheath.Play();
            collision.gameObject.transform.parent = gameObject.transform;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.transform.position = targetTransform.position;
            collision.gameObject.transform.rotation = targetTransform.rotation;
            collision.gameObject.transform.localScale = targetTransform.localScale;
        }

    }

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
