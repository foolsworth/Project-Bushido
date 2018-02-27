using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Photon.PunBehaviour
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    static public GameManager Instance;


    private void Start()
    {

        Instance = this;

        if (!PhotonNetwork.connected)
        {
            SceneManager.LoadScene("Game");

            return;
        }

        if (GameManager.LocalPlayerInstance == null)
        {
            Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.Log("Ignoring scene load for " + SceneManager.GetActiveScene().name);
        }
    }

    private void Awake()
    {
        /* // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (PhotonNetwork.player.IsLocal)
        {
            GameManager.LocalPlayerInstance = GameObject.FindGameObjectWithTag("local");
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);*/

    }

    void LoadScene()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
       
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadScene();
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadScene();
        }
    }

}
