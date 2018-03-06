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
            gameObject.transform.Find("head").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("left").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("right").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("right").GetComponent<grabsword>().enabled = false;
            return;
        }





    }

}
