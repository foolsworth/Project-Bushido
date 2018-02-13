using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionFlower : MonoBehaviour
{
    int count;
    GameObject flower;
    public GameObject prefab;
    // Use this for initialization
    void Start()
    {
        flower = Instantiate(prefab) as GameObject;
        flower.transform.localScale = gameObject.transform.localScale;
        flower.transform.position = gameObject.transform.position;
        flower.transform.rotation = gameObject.transform.rotation;
        //flower.transform.parent = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count >= 100)
        {
            flower.transform.localScale = gameObject.transform.localScale;
            flower.transform.position = gameObject.transform.position;
            flower.transform.rotation = gameObject.transform.rotation;
        }

    }
}
