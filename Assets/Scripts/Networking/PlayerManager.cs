
using UnityEngine;
using UnityEngine.EventSystems;



public class PlayerManager : Photon.PunBehaviour
{

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;



    public void Awake()
    {

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if (photonView.isMine)
        {
            LocalPlayerInstance = gameObject;
            LocalPlayerInstance.tag = "local";
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);
    }

}
