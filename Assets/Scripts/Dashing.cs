using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {

    public Transform GhostTransform;

    public AudioSource noDashing;

    private bool dashing = false;

    private SteamVR_TrackedObject trackedObj;

    public GameObject physicalBody;

    public Transform physicalBodyTransform;

    public float distance;

    public float catchUpTime;

    private Rigidbody PhysicalBodyRB;

    private Vector3 targetPosition;

    public float dashSpeed;

    public float dashCD;

    private float elapsedTime;
    private bool count = true;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    void Start()
    {

        physicalBodyTransform = physicalBody.transform;

        PhysicalBodyRB = physicalBody.GetComponent<Rigidbody>();

        elapsedTime = dashCD;

    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // Update is called once per frame
    void Update () {
       

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) )
        {
            count = false;
            if(elapsedTime >= dashCD) {
                elapsedTime = 0;
                dashing = true;
                targetPosition = GhostTransform.position + (new Vector3(transform.forward.x, 0, transform.forward.z)) * distance;
            }else
            {
                noDashing.Play();
            }
            
        }
        else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            count = true;
            dashing = false;
            GhostTransform.position = physicalBodyTransform.position;
        }
        
        if (dashing)
        {
            Debug.Log("Dats Dash YO!");
            PhysicalBodyRB.position = Vector3.Lerp(PhysicalBodyRB.position, targetPosition, Time.deltaTime * dashSpeed);
            //if(PhysicalBodyRB.position.magnitude > targetPosition.magnitude-0.5 && PhysicalBodyRB.position.magnitude < targetPosition.magnitude + 1)
            //{
               
            //    GhostTransform.position = targetPosition;
            //}
        }
        if (count)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
        }
        
        }
}
