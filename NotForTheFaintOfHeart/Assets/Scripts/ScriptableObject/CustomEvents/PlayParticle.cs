using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Play Particle Event", menuName = "CustomEvent/PlayParticleEvent", order = 2)]
public class PlayParticle : CustomEvent
{

    public GameObject particleToPlay;

    public bool changeBaseColor = false;

    [HideInInspector]
    public Color newColor = Color.white;

    public override void PlayEvent(GameObject target)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        EventEffect(target);
    }

    public override void EventEffect(GameObject target)
    {
        if (changeBaseColor)
        {
            var main = particleToPlay.GetComponent<ParticleSystem>().main;
            main.startColor = newColor;

        }

        Instantiate(particleToPlay, target.transform.position, target.transform.rotation);

        isPlaying = false;
    }
}

/*
[CustomEditor(typeof(PlayParticle))]
public class PlayParticleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as PlayParticle;

        base.OnInspectorGUI();

        if (myScript.changeBaseColor)
        {
            myScript.newColor = EditorGUILayout.ColorField("New Color",myScript.newColor);

        }
        
    }
}
*/