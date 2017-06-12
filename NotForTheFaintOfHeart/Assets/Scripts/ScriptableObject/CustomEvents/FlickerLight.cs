using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Flicker Light Event", menuName = "CustomEvent/FlickerLightEvent", order = 4)]
public class FlickerLight : CustomEvent
{
    public enum LightRangeType
    {
        NEAREST,
        RANGE,
        ALL
    }

    public LightRangeType lightType;
    public float flickerCount = 5;

    [HideInInspector]
    public float range;

    public virtual void PlayEvent(GameObject[] targets)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        EventEffect(targets[0]);
    }

    public override void EventEffect(GameObject target)
    {
        LightController[] allLights = FindObjectsOfType<LightController>();
        switch (lightType)
        {
            case LightRangeType.NEAREST:
                
                float tempDist = 99999;
                LightController nearestL = null;
                foreach (LightController l in allLights)
                {
                    float curDist = Vector3.Distance(target.transform.position, l.gameObject.transform.position);
                    
                    if (nearestL == null)
                    {
                        nearestL = l;
                        tempDist = curDist;
                    }
                    if(nearestL != null)
                    {
                        float curNearestL = Vector3.Distance(target.transform.position, nearestL.gameObject.transform.position);
                        if (curNearestL < tempDist)
                        {
                            nearestL = l;
                            tempDist = curDist;
                        }
                    }
                }

                nearestL.Flicker(flickerCount);

                break;

            case LightRangeType.RANGE:
                List<LightController> inRangeController = new List<LightController>();
                foreach (LightController l in allLights)
                {
                    float currDist = Vector3.Distance(target.gameObject.transform.position, l.gameObject.transform.position);
                    if (currDist <= range)
                    {
                        inRangeController.Add(l);
                    }
                }

                foreach (LightController b in inRangeController)
                {
                    b.Flicker(flickerCount);
                }

                break;

            case LightRangeType.ALL:
                foreach (LightController l in allLights)
                {
                    l.Flicker(flickerCount);
                }
                break;

            default:
                ErrorMe("No Light Range of the type.");
                break;
        }

        isPlaying = false;
    }
}

/*
[CustomEditor(typeof(PlayParticle))]
public class FlickerLightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as FlickerLight;

        base.OnInspectorGUI();

        if (myScript.lightType == FlickerLight.LightRangeType.RANGE)
        {
            myScript.range = EditorGUILayout.FloatField("Range :", myScript.range);

        }
        
    }
}
*/