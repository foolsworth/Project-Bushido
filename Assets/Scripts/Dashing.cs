using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {

    public Transform GhostTransform;

    public AudioSource noDashing;

    public bool dashing = false;

    public bool kb = false;
    public bool kbreset = false;
    public Vector3 pos;

    private SteamVR_TrackedObject trackedObj;

     GameObject physicalBody;

    Transform physicalBodyTransform;

    public float distance;

    public float catchUpTime;

    private Rigidbody PhysicalBodyRB;

    private Vector3 targetPosition;

    public float dashSpeed;

    public float dashCD;

    private float playerHeight;

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

    public void Knockback(Vector3 pos)
    {
        kb = true;
        this.pos=pos;
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
                physicalBodyTransform.position = GhostTransform.position;
                instantiated = true;
            }
        }

        if (kb && !kbreset)
        {
            dashing = true;
            targetPosition = GhostTransform.position + new Vector3(pos.x,0,pos.z);
            
            Controller.TriggerHapticPulse(100);
            kb = false;
            kbreset = true;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) )
        {
            count = false;
            if(elapsedTime >= dashCD) {
                //PhysicalBodyRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                elapsedTime = 0;
                dashing = true;
                Vector2 temp = new Vector2(Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x, Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
                Debug.Log("(" + temp.x + ", " + temp.y + ")");
                float angle = -Vector2.SignedAngle(Vector2.up, temp);
                //"You can't put find()s in the update method." "I'm just testing right now."
                //replace the 'physicalBodyTransform.Find("head").transform' with a 'theHead'
                Vector3 relativedir = Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(physicalBodyTransform.Find("head").transform.forward.normalized.x, 100, physicalBodyTransform.Find("head").transform.forward.normalized.z) * distance;
                targetPosition = GhostTransform.position + relativedir;
                RaycastHit hit;
                Ray downRay = new Ray(transform.position, -Vector3.up);
                if (Physics.Raycast(downRay, out hit))
                {
                    playerHeight = transform.position.y - hit.transform.position.y;
                }
                RaycastHit hit2;
                Ray downRay2 = new Ray(targetPosition, -Vector3.up);
                if (Physics.Raycast(downRay2, out hit2))
                {
                    targetPosition = new Vector3(targetPosition.x,hit2.transform.position.y+playerHeight/2,targetPosition.z);
                }

                //Kevin wishes to revisit the below
                //Vector3 dashDir = new Vector3(Controller.GetAxis().x, 0.0f, Controller.GetAxis().y);
                //Debug.Log(dashDir);
                //Vector3 myForward = Vector3.ProjectOnPlane(theHead.forward, Vector3.up);
                //Debug.Log(myForward);
                //float angle = Vector3.SignedAngle(myForward, dashDir, Vector3.up);
                //Quaternion rotation = Quaternion.Euler(0.0f, angle, 0.0f);
                //Vector3 temp = targetPosition;
                //targetPosition = GhostTransform.position + rotation * dashDir.normalized * distance;
                //Debug.Log("delta is: " + (targetPosition - temp));
                //Debug.Log("==========");
            }
            else
            {
                noDashing.Play();
            }
            
        }
        else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && instantiated)
        {
            //if (dashing)
            //{
            //    PhysicalBodyRB.constraints = RigidbodyConstraints.FreezeAll;
            //}
            count = true;
            dashing = false;
            //RaycastHit hit3;
            //Ray downRay3 = new Ray(physicalBodyTransform.position+new Vector3(0,100,0), -Vector3.up);
            //if (Physics.Raycast(downRay3, out hit3))
            //{
            //    if (!hit3.transform.name.Contains("physicalBody"))
            //    {
            //        physicalBodyTransform.position = new Vector3(physicalBodyTransform.position.x, hit3.transform.position.y + playerHeight / 2, physicalBodyTransform.position.z);
            //    }
            //}
            GhostTransform.position = physicalBodyTransform.position;
            
        }
        
        if (dashing && instantiated)
        {
            Debug.Log("Dats Dash YO!");
            PhysicalBodyRB.position =new Vector3( Mathf.Lerp(PhysicalBodyRB.position.x, targetPosition.x, Time.deltaTime * dashSpeed), PhysicalBodyRB.position.y, Mathf.Lerp(PhysicalBodyRB.position.z, targetPosition.z, Time.deltaTime * dashSpeed));

            if(kbreset && Vector3.Distance(PhysicalBodyRB.position, targetPosition) <= 0.04)
            {
                GhostTransform.position = physicalBodyTransform.position;
                kbreset = false;
            }
            //if(PhysicalBodyRB.position.magnitude > targetPosition.magnitude-0.5 && PhysicalBodyRB.position.magnitude < targetPosition.magnitude + 1)
            //{

            //    GhostTransform.position = targetPosition;
            //}
        }

        //if (!dashing)
        //{
        //    GhostTransform = physicalBody.transform;
        //}

        if (count)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);
        }
        
        }
}
