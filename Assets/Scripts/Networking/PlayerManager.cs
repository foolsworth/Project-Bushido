
using UnityEngine;
using UnityEngine.EventSystems;



public class PlayerManager : Photon.PunBehaviour
{



    public void Awake()
    {

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
        if (photonView.isMine)
        {
            GameManager.LocalPlayerInstance = gameObject;
            GameManager.LocalPlayerInstance.tag = "local";
        }

        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);
    }

}
