using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    public AudioSource aSource;

    [Range(0.01f,10f)]
    public float fadeTime = 1f;
    public float targetVolume = 0.1f;

    AudioClip aCur;
    [SerializeField]
    bool startFade = false;

    void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (startFade && aCur != null)
        {
            StartCoroutine(TransitionMusic(aCur, targetVolume));
        }
    }

    public void PlaySong(AudioClip aClip)
    {
        if (aSource != null)
        {
            StartCoroutine(FadeOut());
                aCur = aClip;
        }
    }

    public void PlaySong(AudioClip aClip, float targVolume)
    {
        if (aSource != null)
        {
            StartCoroutine(FadeOut());
                aCur = aClip;
                targetVolume = targVolume;
        }
    }

    private IEnumerator FadeOut()
    {
        float startVol = aSource.volume;

        // Fade out
        while (aSource.volume > 0)
        {

            aSource.volume -= startVol * Time.deltaTime / fadeTime;
            yield return null;
        }

        if (aSource.volume <= 0)
            startFade = true;

        aSource.Stop();

    }

    private IEnumerator TransitionMusic(AudioClip newClip, float targVolume)
    {
        float startVol = aSource.volume;


        if (!aSource.isPlaying)
        {
            aCur = null;
            startFade = false;
            aSource.clip = newClip;
            aSource.Play();
        }

        // Fade out
        while (aSource.volume <= targVolume)
        {
            aSource.volume += Time.deltaTime / fadeTime;

            yield return null;
        }

        //

        //startVol = aSource.volume;

 

        //aSource.volume = startVol;

        //while (aSource.volume <= targVolume)
        //{
        //    aSource.volume += startVol * Time.deltaTime / fadeTime;

        //}




    }
}
