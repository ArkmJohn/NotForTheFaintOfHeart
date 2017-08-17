using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float timeTillDestroy;
    public AudioClip clip;
    public float targVol;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, timeTillDestroy);
	}

    void OnDestroy()
    {
       
        if(targVol > 0)
            FindObjectOfType<AudioController>().PlaySong(clip, targVol);
        else
            FindObjectOfType<AudioController>().PlaySong(clip);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
