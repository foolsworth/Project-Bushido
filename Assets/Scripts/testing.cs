using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        StartCoroutine(startme());
	}
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator startme()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Launcher");
    }
}
