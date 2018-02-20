﻿using UnityEngine;


namespace Bushido
{
    public class Launcher : Photon.PunBehaviour
    {


        /// <summary>
                /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
                /// </summary>
        string _gameVersion = "1";

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>   
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;

        bool isConnecting;

        /// <summary>
                /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
                /// </summary>
        void Awake()
        {


            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;


            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }


        /// <summary>
                /// MonoBehaviour method called on GameObject by Unity during initialization phase.
                /// </summary>
        void Start()
        {
            Connect();
        }



        /// <summary>
                /// Start the connection process. 
                /// - If already connected, we attempt joining a random room
                /// - if not yet connected, Connect this application instance to Photon Cloud Network
                /// </summary>
        public void Connect()
        {

            isConnecting = true;
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.connected)
            {
                if (isConnecting)
                {
                    // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
                    PhotonNetwork.JoinRandomRoom();
                }
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

      
        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()  
            PhotonNetwork.JoinRandomRoom();
        }


        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            if (PhotonNetwork.room.PlayerCount == 1)
            {


                // #Critical
                // Load the Room Level. 
                PhotonNetwork.LoadLevel("Game");
            }
        }

    }
}
