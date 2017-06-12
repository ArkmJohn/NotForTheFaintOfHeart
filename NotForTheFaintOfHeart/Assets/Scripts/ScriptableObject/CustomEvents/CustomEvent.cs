using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public abstract class CustomEvent : ScriptableObject
{
    [SerializeField]
    protected bool isPlaying = false;

    public virtual void PlayEvent(GameObject target)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        ErrorMe("No Effect Played.");
        isPlaying = false;
    }

    public abstract void EventEffect(GameObject target);

    public void ErrorMe(string extraMessage)
    {
        Debug.LogError("There is an error in " + this.GetType().Name + ". " + extraMessage);
    }

    public bool eventIsPlaying()
    {
        return isPlaying;
    }
}
