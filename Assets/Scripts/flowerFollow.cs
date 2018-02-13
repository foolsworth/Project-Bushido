using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerFollow : MonoBehaviour
{
    public Transform tree;


    void Start()
    {
    }
    void Update()
    {
        if (tree)
        {
            transform.position = tree.position;
            transform.rotation = tree.rotation;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
