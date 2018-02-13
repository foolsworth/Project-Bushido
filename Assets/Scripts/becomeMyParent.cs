using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class becomeMyParent : MonoBehaviour {
    public GameObject parent;
    int count;
	// Use this for initialization
	void OnEnable () {
        
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count == 10)
        {
            gameObject.transform.parent = parent.transform;
        }
	}
}
