using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigator : MonoBehaviour {
    //I'm thinking this script should be on the candle or on the flame.

    public SceneTransitionScript_v3 m_transitionController;
    bool m_selectionMade;
    public AsyncOperation m_LoadingNextScene;
    public string m_NextSceneName = "Game";

    public GameObject Launcher;

    private void Awake() {
        Debug.Log("Menu awake ...");
    }

    private void Start() {
        Debug.Log("Menu started...?");
        m_selectionMade = false;

        //set the variables to infinity
        //m_transitionController.m_waitTime = float.PositiveInfinity;
        //m_transitionController.m_animationDuration = float.PositiveInfinity;
        //pause the transition
        if (!m_transitionController) {
            m_transitionController = (SceneTransitionScript_v3)GameObject.Find("PropParent").GetComponent(typeof(SceneTransitionScript_v3));
            Debug.Log(m_transitionController);
        }
        //Begin loading the next scene
        m_LoadingNextScene = SceneManager.LoadSceneAsync(m_NextSceneName, LoadSceneMode.Additive);
        m_transitionController.PauseTransition();
    }

    private void OnTriggerEnter(Collider p_collider) {
        if (p_collider.transform.CompareTag("Menu_Proceed")) {
            //do the proceed thing
            //load the scene or whatever.
            Debug.Log("Menu option \"Proceed\" was selected.");
            m_selectionMade = true;

            //Check if the scene has loaded
            /**/if (m_LoadingNextScene.isDone) {/*/
                //unpause the transition
                m_transitionController.PauseTransition(true);/*/
                m_transitionController.PauseTransition(true);
            }/**/
        }
        if(p_collider.transform.CompareTag("Menu_Option0")) {
            //do the options thing
            //load the scene or whatever
            Debug.Log("Menu option \"Options\" was selected.");
            m_transitionController.PauseTransition(true);
        }
        //Light the component in question
        //p_collider.gameObject.GetComponent<Component>().DoGlowingOrLightOnFire();
    }

    private void Update() {
        if (m_LoadingNextScene != null) { 
        if (m_LoadingNextScene.isDone)
        {
            m_transitionController.PauseTransition(m_selectionMade);  //unpause if argument is true, selection made means we should unpause
            //Launcher.GetComponent<OurLauncher>().Connect();
        } }
        if (m_transitionController.m_isDone)
        {
            //m_LoadingNextScene = SceneManager.LoadSceneAsync(m_NextSceneName, LoadSceneMode.Additive);
            Launcher.GetComponent<OurLauncher>().Connect();
            m_LoadingNextScene.allowSceneActivation = true;
            
        }
        /*
        if(m_LoadingNextScene.isDone && m_transitionController.m_isDone) {
            
            SceneManager.SetActiveScene(m_LoadingNextScene)
        }*/
    }

    private void setTransitionLength(float givenDuration) {
        m_transitionController.m_animationDuration = givenDuration;
    }
}
