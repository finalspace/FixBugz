using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleManager : MonoBehaviour {

    public FadeInOut Text;

    public string titleSceneName = "TitleScene";

    private bool readyToStart = false;

	// Use this for initialization
	void Start () {
        Text.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.UnloadSceneAsync(titleSceneName);
        }
	}
}
