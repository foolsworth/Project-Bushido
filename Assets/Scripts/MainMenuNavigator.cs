using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigator : MonoBehaviour {
    //I'm thinking this script should be on the candle or on the flame.

    public SceneTransitionScript_v3 m_transitionController;

    private void Start() {
        
        //set the variables to infinity
        //m_transitionController.m_waitTime = float.PositiveInfinity;
        //m_transitionController.m_animationDuration = float.PositiveInfinity;
        //pause the transition
        m_transitionController.PauseTransition();
    }

    private void OnTriggerEnter(Collider p_collider) {
        if (p_collider.transform.CompareTag("Menu_Proceed")) {
            //do the proceed thing
            //load the scene or whatever.
            Debug.Log("Menu option \"Proceed\" was selected.");
            //unpause the transition
            m_transitionController.PauseTransition(true);

        }
        if(p_collider.transform.CompareTag("Menu_Option0")) {
            //do the options thing
            //load the scene or whatever
            Debug.Log("Menu option \"Options\" was selected.");
            m_transitionController.PauseTransition(true);
        }
    }

    private void setTransitionLength(float givenDuration) {
        m_transitionController.m_animationDuration = givenDuration;
    }
}
