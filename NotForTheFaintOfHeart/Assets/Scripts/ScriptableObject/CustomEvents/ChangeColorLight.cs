using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Change Color Light Event", menuName = "CustomEvent/ChangeColorLightEvent", order = 5)]
public class ChangeColorLight : CustomEvent
{
    public enum LightRangeType
    {
        NEAREST,
        RANGE,
        ALL
    }

    public LightRangeType lightRangeType;

    public Color newColor = Color.white;

    [HideInInspector]
    public float range;

    public override void PlayEvent(GameObject target)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        EventEffect(target);
    }

    public override void EventEffect(GameObject target)
    {
        LightController[] allLights = FindObjectsOfType<LightController>();
        switch (lightRangeType)
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
                    if (nearestL != null)
                    {
                        float curNearestL = Vector3.Distance(target.transform.position, nearestL.gameObject.transform.position);
                        if (curNearestL < tempDist)
                        {
                            nearestL = l;
                            tempDist = curDist;
                        }
                    }
                }

                nearestL.ChangeColor(newColor);

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
                    b.ChangeColor(newColor);
                }

                break;

            case LightRangeType.ALL:
                foreach (LightController l in allLights)
                {
                    l.ChangeColor(newColor);
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
[CustomEditor(typeof(ChangeColorLight))]
public class ChangeColorLightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as ChangeColorLight;

        base.OnInspectorGUI();

        if (myScript.lightRangeType == ChangeColorLight.LightRangeType.RANGE)
        {
            myScript.range = EditorGUILayout.FloatField("Range :", myScript.range);

        }
    }
}
*/