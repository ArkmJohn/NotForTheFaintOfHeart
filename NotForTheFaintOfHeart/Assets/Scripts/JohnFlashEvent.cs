using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnFlashEvent : MonoBehaviour {

	public GameObject stillModel;

	// Use this for initialization
	void Start () 
	{
		stillModel.SetActive (false);
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.gameObject.GetComponent<FlashlightControl> () != null) 
		{
			stillModel.SetActive (true);
			Destroy (this.gameObject);
		}
	}
}
