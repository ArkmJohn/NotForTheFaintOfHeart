using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour {

    public float timeTilDeath = 5;    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeTilDeath > 0)
        {
            timeTilDeath -= Time.deltaTime;
        }
        if (timeTilDeath < 0)
        {
            this.gameObject.SetActive(false);
        }
	}
}
