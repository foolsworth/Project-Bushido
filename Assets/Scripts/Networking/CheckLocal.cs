using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocal : Photon.MonoBehaviour
{
    bool set = false;
    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            gameObject.tag = "local";
        }
        LookatCamera.targets.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine && !set)
        {

            gameObject.transform.Find("head").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("left").GetComponent<lerpGhostHands>().enabled = false;
            gameObject.transform.Find("right").GetComponent<lerpGhostHands>().enabled = false;
            //gameObject.transform.Find("right").GetComponent<grabsword>().enabled = false;

            gameObject.transform.Find("headM").gameObject.layer = 13;
            gameObject.transform.Find("bodyM").gameObject.layer = 13;
            gameObject.transform.Find("bodyM").Find("Tsujigiri_Blade_Textured").gameObject.layer = 11;
            gameObject.transform.Find("bodyM").Find("Tsujigiri_Blade_Textured").Find("swordCollider").gameObject.layer = 11;
            Transform temp = gameObject.transform.Find("bodyM").Find("Tsujigiri_Blade_Textured").Find("swordCollider").transform;
            foreach (Transform child  in temp)
            {
                child.gameObject.layer = 11;
            }
            
            gameObject.transform.Find("rightM").gameObject.layer = 13;
            gameObject.transform.Find("rightM").gameObject.tag = "handE";

            gameObject.transform.Find("rightM").Find("Tsujigiri_FirstHero_HandGauntlet_RIGHT").gameObject.layer = 13;
            gameObject.transform.Find("leftM").gameObject.layer = 13;
            gameObject.transform.Find("leftM").Find("Tsujigiri_FirstHero_HandGauntlet_LEFT").gameObject.layer = 13;

            set = true;
        }





    }

}
