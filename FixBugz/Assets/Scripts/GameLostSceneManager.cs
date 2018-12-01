using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLostSceneManager : MonoBehaviour {

    public FadeInOut OutroScreen;
    public FadeInOut Text;

    private bool readyToRestart = false;

	// Use this for initialization
	void Start () {
        OutroScreen.Play();
        Text.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!readyToRestart)
            return;

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel("Main");
        }
	}

    public void ReadyToRestart()
    {
        readyToRestart = true;
    }
}
