using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float timeTillDestroy;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, timeTillDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
