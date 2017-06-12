using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Grouped Event", menuName = "CustomEvent/GroupedEvent", order = 7)]
public class GroupedEvent : CustomEvent
{

    public List<CustomEvent> eventList = new List<CustomEvent>();

    public override void PlayEvent(GameObject target)
    {
        isPlaying = true;
        foreach (CustomEvent c in eventList)
        {
            c.PlayEvent(target);
        }
        isPlaying = false;
    }

    public override void EventEffect(GameObject target)
    {
    }
}

