using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectHeight : MonoBehaviour {

    Transform body, bodyArea, ghostArea;
    public GameObject dashHand;
    bool ishere = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!ishere)
        {
            if (GameObject.FindGameObjectWithTag("local") != null)
            {
                ghostArea = GameObject.Find("GhostBody").transform;
                bodyArea = GameObject.FindGameObjectWithTag("local").transform;
                transform.position = bodyArea.position;
                body = bodyArea.Find("body");
                ishere = true;
            }
        }
        if (bodyArea != null )
        {
            transform.position = new Vector3(body.position.x, transform.position.y, body.position.z);
            if (!dashHand.GetComponent<Dashing>().dashing)
            {
                bodyArea.position = new Vector3(bodyArea.position.x, transform.position.y, bodyArea.position.z);
                ghostArea.position = new Vector3(bodyArea.position.x, transform.position.y, bodyArea.position.z);
            }
            else
            {
                bodyArea.position = new Vector3(bodyArea.position.x, transform.position.y, bodyArea.position.z);
            }
        }
	}
}
