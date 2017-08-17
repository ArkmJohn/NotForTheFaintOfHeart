using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnFlashEvent : MonoBehaviour {

	public GameObject stillModel;
    public AudioSource aSource;
    public AudioClip play;

	// Use this for initialization
	void Start () 
	{
		stillModel.SetActive (false);
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.tag == "Player") 
		{
			stillModel.SetActive (true);
            aSource.PlayOneShot(play);
			Destroy (this.gameObject);
		}
	}
}
