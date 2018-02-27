using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpGhostHands : MonoBehaviour {

    Transform hand;
    public int updateSpeed = 1;
    bool instantiated = false;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!instantiated)
        {
            if (gameObject.transform.parent.tag=="local")
            {
                
                
                if (gameObject.transform.name == "head")
                {
                    hand = GameObject.Find("GhostBody").transform.Find("Camera (eye)");
                }
                else
                {
                    hand = GameObject.Find("GhostBody").transform.Find("Controller (" + gameObject.transform.name + ")");
                }
                    instantiated = true;
            }
        }
        if (gameObject.transform.parent.tag == "local")
        {
            if (transform.localPosition != hand.localPosition)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, hand.localPosition, Time.deltaTime * updateSpeed);
            }
            if (transform.localRotation != hand.localRotation)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, hand.localRotation, Time.deltaTime * updateSpeed);
            }
        }
    }
}
