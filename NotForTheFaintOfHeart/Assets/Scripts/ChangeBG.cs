using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour {

    public AudioClip clip;
    public float targVol;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (targVol > 0)
                FindObjectOfType<AudioController>().PlaySong(clip, targVol);
            else
                FindObjectOfType<AudioController>().PlaySong(clip);
        }
    }
}
