﻿
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using ExitGames.Client.Photon;


    public class OurGameManager : Photon.PunBehaviour
{

        #region Public Variables

        static public OurGameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        #endregion

        #region Private Variables

        private GameObject instance;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            Instance = this;
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            //if (!PhotonNetwork.connected)
            //{
            //    SceneManager.LoadScene("Launcher");

            //    return;
            //}

            if (playerPrefab == null)
            { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

                Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {


                if (OurPlayerManager.LocalPlayerInstance == null)
                {
                    Debug.Log("We are Instantiating LocalPlayer from " + SceneManagerHelper.ActiveSceneName);

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                if (PhotonNetwork.countOfPlayers <= 1)
                {
                    GameObject.Find("Spectator").SetActive(false);
                    PhotonNetwork.Instantiate(this.playerPrefab.name, GameObject.Find("spawnPos1").transform.position, Quaternion.identity, 0);
                    
                }
                else if (PhotonNetwork.countOfPlayers == 2)
                {
                    GameObject.Find("Spectator").SetActive(false);
                    PhotonNetwork.Instantiate(this.playerPrefab.name, GameObject.Find("spawnPos2").transform.position, Quaternion.identity, 0);
                }
                else if (PhotonNetwork.countOfPlayers == 3)
                {
                    GameObject.Find("[SteamVR]").SetActive(false);
                    GameObject.Find("GhostBody").SetActive(false);

                }
            }
                else
                {

                    Debug.Log("Ignoring scene load for " + SceneManagerHelper.ActiveSceneName);
                }


            }

        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            // "back" button of phone equals "Escape". quit app if that's pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }

        #endregion

        #region Photon Messages

        /// <summary>
        /// Called when a Photon Player got connected. We need to then load a bigger scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public override void OnPhotonPlayerConnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected

                //LoadArena();
            }
        }

        /// <summary>
        /// Called when a Photon Player got disconnected. We need to load a smaller scene.
        /// </summary>
        /// <param name="other">Other.</param>
        public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // seen when other disconnects

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected

                //LoadArena();
            }
        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Launcher");
        }

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        #endregion

        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }

            Debug.Log("PhotonNetwork : Loading Level : Game");

            PhotonNetwork.LoadLevel("Game");
        }

    #endregion

    
}

