using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) // Interactable Fuction
        {
            PlayTheGame();
        }

        
    }

    public void PlayTheGame()
    {
        Application.LoadLevel("GameScene");
    }

}
