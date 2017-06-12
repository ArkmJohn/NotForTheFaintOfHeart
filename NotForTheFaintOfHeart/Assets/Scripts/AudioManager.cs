using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;
    public AudioMixer myMixer;
    public AudioMixerSnapshot normalSnap, lowerSnap;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayOneSound(AudioClip myClipToPlay, AudioSource mySource)
    {
        mySource.PlayOneShot(myClipToPlay);
    }

    public void PlayOneSound(AudioClip myClipToPlay, AudioSource mySource, GameObject sub)
    {
        mySource.PlayOneShot(myClipToPlay);
        sub.SetActive(true);
    }

    public void LowerSound()
    {
        lowerSnap.TransitionTo(0.01f);
    }

    public void ToNormal()
    {
        normalSnap.TransitionTo(0.01f);
    }
}
