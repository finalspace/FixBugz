using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyMainSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
	}
}
