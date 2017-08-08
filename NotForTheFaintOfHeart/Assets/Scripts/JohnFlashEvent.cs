using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnFlashEvent : MonoBehaviour {

	GameObject player;
	public GameObject stillJohn;
	bool istriggered;
	public float timeToDestroy;

	// Use this for initialization
	void Start () 
	{
		stillJohn.SetActive (false);
		istriggered = false;
		player = FindObjectOfType<FlashlightControl> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (istriggered) 
		{
			stillJohn.SetActive (true);
			Destroy (stillJohn, timeToDestroy);
		}
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.GetComponent<FlashlightControl> () != null) 
		{
			istriggered = true;
		}
	}
}
