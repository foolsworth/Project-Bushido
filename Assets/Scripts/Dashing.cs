using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {

    public Transform GhostTransform;

    public AudioSource noDashing;

    private bool dashing = false;

    private SteamVR_TrackedObject trackedObj;

     GameObject physicalBody;

    Transform physicalBodyTransform;

    public float distance;

    public float catchUpTime;

    private Rigidbody PhysicalBodyRB;

    private Vector3 targetPosition;

    public float dashSpeed;

    public float dashCD;

    private float elapsedTime;
    private bool count = true;

    bool instantiated = false;

    private Transform theHead;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    void Start()
    {
        theHead = physicalBodyTransform.Find("head").transform;
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // Update is called once per frame
    void Update () {
       
        if (!instantiated)
        {
            if (GameObject.FindGameObjectWithTag("local") != null)
            {
                physicalBody = GameObject.FindGameObjectWithTag("local");

                physicalBodyTransform = physicalBody.transform;

                PhysicalBodyRB = physicalBody.GetComponent<Rigidbody>();

                
                physicalBody.transform.position = GhostTransform.position;
                physicalBody.transform.rotation = GhostTransform.rotation;

                instantiated = true;
            }
        }
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) )
        {
            count = false;
            if(elapsedTime >= dashCD) {
                elapsedTime = 0;
                dashing = true;
                Vector2 temp = new Vector2(Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x, Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
                Debug.Log("(" + temp.x + ", " + temp.y + ")");
                float angle = -Vector2.SignedAngle(Vector2.up, temp);
                //"You can't put find()s in the update method." "I'm just testing right now."

                Vector3 relativedir = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(physicalBodyTransform.Find("head").transform.forward.normalized.x, 0, physicalBodyTransform.Find("head").transform.forward.normalized.z) * distance;
                targetPosition = GhostTransform.position + relativedir;

                //Vector3 dashDir = new Vector3(Controller.GetAxis().x, 0.0f, Controller.GetAxis().y);
                //Vector3 myForward = Vector3.ProjectOnPlane(theHead.forward, Vector3.up);
                //float kAngle = Vector3.SignedAngle()
            }
            else
            {
                noDashing.Play();
            }
            
        }
        else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && instantiated)
        {
            count = true;
            dashing = false;
            GhostTransform.position = physicalBodyTransform.position;
        }
        
        if (dashing && instantiated)
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
