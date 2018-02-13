using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerEffect2 : MonoBehaviour
{
    public GameObject pivot;
    public GameObject petal;
    public GameObject petalPivot;

    int counter = 0;
    int random = 5;
    int reversal = 1;
    Vector3 origin, origin2;
    static float angle, angle2;
    // Use this for initialization
    void Start()
    {
        origin = gameObject.transform.up;
        origin2 = petal.transform.up;
        reversal = -1;
    }

    // Update is called once per frame
    void Update()
    {

        //if(Quaternion.Angle(gameObject.transform.rotation, origin) > 10)
        //{
        //    random = Random.Range(5, 20);
        //    reversal *= -1;
        //}

        //float angle = Vector3.Angle(origin, gameObject.transform.up);
        //Vector3 cross = Vector3.Cross(gameObject.transform.up, origin);
        //if (cross.y < 0 && angle > 0) angle = -angle;
        angle = Vector3.SignedAngle(origin, gameObject.transform.up, pivot.transform.up);
        angle2 = Vector3.SignedAngle(origin2, petal.transform.up, petalPivot.transform.up);

        if (angle >= angle2-5)
        {
            random = Random.Range(1, 5);
            Debug.Log("nega");
            reversal = -1;
        }

        if (angle < -10.0)
        {
            Debug.Log("posa");
            random = Random.Range(1, 5);
            reversal = +1;
        }
        Debug.Log(reversal);
        Debug.Log(angle);
        //counter++;
        //if(counter%100 == 0)
        //{
        //    if (reversal == -1)
        //        random = Random.Range(5, 50);
        //    reversal *= -1;

        //}
        transform.RotateAround(pivot.transform.position, pivot.transform.up, reversal * random * Time.deltaTime);
    }
}
