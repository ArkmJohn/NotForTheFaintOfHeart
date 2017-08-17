using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

    public AudioSource aSource;
    public AudioClip gotchaSound, endSound, startScream, chaseSound;

    public AudioClip[] randomNoise;
    public bool screamDone = false;

    void Awake()
    {
        if (aSource == null)
        {
            aSource = gameObject.AddComponent<AudioSource>();

        }
        aSource.spatialBlend = 1;
        aSource.maxDistance = 5;
    }

    #region Anim.Events

    public void Gotcha()
    {
        if (gotchaSound != null)
        {
            PlaySound(gotchaSound, 1);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Exit", LoadSceneMode.Single);
    }

    public void StartScream()
    {
        if (startScream != null)
        {
            PlaySound(startScream, 1);
            screamDone = true;
        }
    }

    public void INYou()
    {
        if (screamDone)
        {
            if (chaseSound != null)
            {
                PlaySound(chaseSound, 1);
            }
        }
    }
    #endregion

    #region Sound Stuff

    void PlaySound(AudioClip clip)
    {
        aSource.PlayOneShot(clip, Random.Range(0.01f, 1f));
    }

    void PlaySound(AudioClip clip, float vol)
    {
        aSource.PlayOneShot(clip, vol);
    }

    #endregion
}
