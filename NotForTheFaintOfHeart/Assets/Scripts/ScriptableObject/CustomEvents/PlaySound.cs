using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Play Sound Event", menuName = "CustomEvent/PlaySoundEvent", order = 1)]
public class PlaySound : CustomEvent
{

    public enum AudioType
    {
        LOOP,
        ONESHOT
    }

    public AudioClip clipToPlay;

    public AudioType audioType;
    public bool isRandom = false;

    [HideInInspector]
    public float volume = 1;

    [HideInInspector]
    public float minVolumeValue = 0;
    [HideInInspector]
    public float maxVolumeValue = 1;



    public override void PlayEvent(GameObject target)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        EventEffect(target);
        
    }

    public override void EventEffect(GameObject target)
    {
        if (target.GetComponent<AudioSource>() != null)
        {
            // Do Stuff Here
            AudioSource myAS = target.GetComponent<AudioSource>();

            if (isRandom)
            {
                float randomValue = UnityEngine.Random.Range(minVolumeValue, maxVolumeValue);
                myAS.volume = randomValue;
            }
            else
            {
                myAS.volume = volume;
            }

            switch (audioType)
            {
                case AudioType.LOOP:
                    myAS.clip = clipToPlay;
                    myAS.loop = true;
                    myAS.Play();
                    isPlaying = false;
                    break;

                case AudioType.ONESHOT:
                    myAS.PlayOneShot(clipToPlay);
                    myAS.loop = false;
                    isPlaying = false;
                    break;

                default:
                    isPlaying = false;
                    ErrorMe("Not a valid AudioType.");
                    break;
            }

        }
        else
        {
            ErrorMe("No Audio Source Detected for the event");
        }
    }

}

//[CustomEditor(typeof(PlaySound))]
//public class PlaySoundEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        var myScript = target as PlaySound;

//        base.OnInspectorGUI();

//        if (myScript.isRandom)
//        {
//            myScript.minVolumeValue = EditorGUILayout.FloatField("Minimum Volume :", myScript.minVolumeValue);
//            myScript.maxVolumeValue = EditorGUILayout.FloatField("Maximum Volume :", myScript.maxVolumeValue);

//        }
//        else
//        {
//            //myScript.volume = EditorGUILayout.FloatField("Volume :", myScript.volume);
//            EditorGUILayout.LabelField("Volume");
//            myScript.volume = EditorGUILayout.Slider(myScript.volume, 0, 100);
//        }
//    }
//}
