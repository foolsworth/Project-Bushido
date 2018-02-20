using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocal : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (photonView.isMine == false && PhotonNetwork.connected == true)
        //{
        //    gameObject.transform.Find("Head").GetComponent<lerpGhostHands>().enabled = false;
        //    gameObject.transform.Find("Left").GetComponent<lerpGhostHands>().enabled = false;
        //    gameObject.transform.Find("Right").GetComponent<lerpGhostHands>().enabled = false;
        //    gameObject.transform.Find("Right").GetComponent<grabsword>().enabled = false;
        //    gameObject.transform.Find("Body").GetComponent<lerpBody>().enabled = false;

        //}

        if (PhotonNetwork.player.IsLocal && PhotonNetwork.connected == true)
        {
            gameObject.tag = "local";
        }

        
    }
}
