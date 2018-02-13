using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserPointer : MonoBehaviour {
    // 1
    public Transform cameraRigTransform;
    // 2
    public GameObject teleportReticlePrefab;
    // 3
    private GameObject reticle;
    // 4
    private Transform teleportReticleTransform;
    // 5
    public Transform headTransform;
    // 6
    public Vector3 teleportReticleOffset;
    // 7
    public LayerMask teleportMask;
    // 8
    private bool shouldTeleport;
    private SteamVR_TrackedObject trackedObj;

    // 1
    public GameObject laserPrefab;
    // 2
    private GameObject laser;
    // 3
    private Transform laserTransform;
    // 4
    private Vector3 hitPoint;

    public GameObject physicalBody;

    public Transform physicalBodyTransform;


    // time it takes for to catch up to physical body
    public float catchUpTime;


    // used for catchup 
    private float elapsedTime;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Start()
    {
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;
        // 1
        reticle = Instantiate(teleportReticlePrefab);
        // 2
        teleportReticleTransform = reticle.transform;

        physicalBodyTransform = physicalBody.transform;

        catchUpTime = 3.0f;
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
    private void Teleport()
    {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5

        // MOVING  the camera to catch up to teleport 
        //cameraRigTransform.position = hitPoint + difference;

        // Moving the ghostbody to the teleport point
        physicalBodyTransform.position = hitPoint;

    }

    private void catchUp()
    {
        cameraRigTransform.position = Vector3.Lerp(cameraRigTransform.position, physicalBodyTransform.position, 1.0f);
    }

    // Update is called once per frame
    void Update () {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            // 2
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {

                if (cameraRigTransform.position == physicalBodyTransform.position)
                {

                    hitPoint = hit.point;
                    ShowLaser(hit);
                    // 1
                    reticle.SetActive(true);
                    // 2
                    teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                    // 3
                    shouldTeleport = true;

                }
            }
        }
        else // 3
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }



        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
            elapsedTime = 0.0f;
        }

        if (cameraRigTransform.position != physicalBodyTransform.position && elapsedTime >= catchUpTime) {

            catchUp();

        }



        elapsedTime += Time.deltaTime;
    }
}
