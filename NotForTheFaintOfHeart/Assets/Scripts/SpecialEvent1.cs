using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEvent1 : MonoBehaviour {

    public AudioClip iCU;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayEvent()
    {
        AudioManager.Instance.PlayOneSound(iCU, GetComponent<AudioSource>());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FlashlightControl>())
        {
            PlayEvent();
        }
    }
}
