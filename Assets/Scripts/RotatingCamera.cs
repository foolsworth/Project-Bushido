using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour {

    public Transform target;
    public int speed;


    // Update is called once per frame
    void Update () {
        if (transform.childCount > 0)
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
            //transform.LookAt(target);
        }
   
    }
}
