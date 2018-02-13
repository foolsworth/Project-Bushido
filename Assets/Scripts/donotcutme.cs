using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donotcutme : MonoBehaviour {
   
    void OnCollisionStay(Collision col)
    {
        Debug.Log("yo");
            
        col.gameObject.GetComponent<Rigidbody>().useGravity = false;
       
    }

}
