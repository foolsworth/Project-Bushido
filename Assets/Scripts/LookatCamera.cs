using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour {

    public static List<Transform> targets;
    Vector3 average; 

	// Use this for initialization
	void Start () {
        targets = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (targets.Count>=2)
        {
            average = (targets[0].position + targets[1].position) / 2;

        }else if(targets.Count == 1)
        {
            average = targets[0].position;
        }
        transform.LookAt(average);
	}
}
