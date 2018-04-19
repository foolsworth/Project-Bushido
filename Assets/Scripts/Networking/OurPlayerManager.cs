

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OurPlayerManager : Photon.PunBehaviour, IPunObservable
    {
        #region Public Variables

       
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

    GameObject spawnPos1;
    GameObject spawnPos2;

    

    bool player1Connected = false;
    bool sceneLoaded = false;
    public static bool dead = false;

    public int checking = 0;
    public bool unsheathed = false;

    public int health = 100;
    #endregion


    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    public void Awake()
        {
        
         
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
            if (photonView.isMine)
            {
                LocalPlayerInstance = gameObject;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        public void Start()
        {
        

           
      

#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }


        public void OnDisable()
        {
#if UNITY_5_4_OR_NEWER
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
#endif
        }



        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// Process Inputs if local player.
        /// Show and hide the beams
        /// Watch for end of game, when local player health is 0.
        /// </summary>
        public void Update()
        {
        // we only process Inputs and check health if we are the local player

        if (SceneManager.GetActiveScene().name == "Game"&& !sceneLoaded)
        {
            spawnPos1 = GameObject.Find("spawnPos1");
            spawnPos2 = GameObject.Find("spawnPos2");

            //if (GameObject.Find("PhysicalBody(Clone)") == null)
            //{

            //    //GameObject.Find("GhostBody").transform.position = spawnPos1.transform.position;
            //    //GameObject.Find("GhostBody").transform.rotation = spawnPos1.transform.rotation;
            //    //player1Connected = true;
            //}

            //else
            //{
            //    GameObject.Find("GhostBody").transform.position = spawnPos2.transform.position;
            //    GameObject.Find("GhostBody").transform.rotation = spawnPos2.transform.rotation;
            //}
            //sceneLoaded = true;
        }
        }

        public void takeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            //if (gameObject.tag == "local")
            //{
            //    GameObject.Find("me").GetComponent<Text>().text = "My health: " + health;

            //}
            //else
            //{
            //    GameObject.Find("you").GetComponent<Text>().text = "Your health: " + health;
            //}
        }
    }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        /*public void OnTriggerEnter(Collider other)
        {
            if (!photonView.isMine)
            {
                return;
            }


            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }

     
        }

        /// <summary>
        /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
        /// We're going to affect health while the beams are interesting the player
        /// </summary>
        /// <param name="other">Other.</param>
        public void OnTriggerStay(Collider other)
        {
            // we dont' do anything if we are not the local player.
            if (!photonView.isMine)
            {
                return;
            }

            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }

         
        }*/


#if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
#endif


        /// <summary>
        /// MonoBehaviour method called after a new level of index 'level' was loaded.
        /// We recreate the Player UI because it was destroy when we switched level.
        /// Also reposition the player if outside the current arena.
        /// </summary>
        /// <param name="level">Level index loaded</param>
        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }

        
           
        }

        #endregion

        #region Private Methods


#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {

            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
#endif

       
        #endregion

        /*
        #region IPunObservable implementation

		void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.isWriting)
			{
				// We own this player: send the others our data
				stream.SendNext(IsFiring);
				stream.SendNext(Health);
			}
            else
            {
				// Network player, receive data
				this.IsFiring = (bool)stream.ReceiveNext();
				this.Health = (float)stream.ReceiveNext();
			}
		}

        #endregion
        */

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
            // We own this player: send the others our data
            stream.SendNext(health);
            stream.SendNext(unsheathed);
            stream.SendNext(checking);
        }
            else
            {
            this.health = (int)stream.ReceiveNext();
            unsheathed = (bool)stream.ReceiveNext();
            checking= (int)stream.ReceiveNext();
        }
        }

        #endregion


   
    }
