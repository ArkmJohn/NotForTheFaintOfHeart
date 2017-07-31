using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnCollisionEnter(Collision collision)
	{
		//SceneManager.LoadScene (1,LoadSceneMode.Single);
		Debug.Log ("Playing Game");
	}
}
