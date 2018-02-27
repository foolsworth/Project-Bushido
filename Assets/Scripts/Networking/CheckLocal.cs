using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocal : Photon.MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            gameObject.tag = "local";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            gameObject.transform.Find("Head").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("Left").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("Right").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("Right").GetComponent<grabsword>().enabled = false;
            gameObject.transform.Find("Body").GetComponent<lerpBody>().enabled = false;
            return;
        }





    }

}
