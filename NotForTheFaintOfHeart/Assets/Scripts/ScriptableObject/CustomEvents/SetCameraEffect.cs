using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[CreateAssetMenu(fileName = "Set Camera Effect Event", menuName = "CustomEvent/CameraEffectEvent", order = 3)]
public class SetCameraEffect : CustomEvent
{
    [Tooltip("The script should be the same to put here on the camera. This is just to detect the script on the camera itself so it can be activated")]
    public string shaderApplicationScriptName;

    public bool enabled = true;
    public bool timedEffect = false;

    [HideInInspector]
    public float timeOfEffectOnPlay = 1;

    public override void PlayEvent(GameObject target)
    {
        isPlaying = true;
        Debug.Log("Playing " + this.GetType().Name);
        EventEffect(target);
    }

    public override void EventEffect(GameObject target)
    {
        GameObject cam = Camera.main.gameObject;

        if (cam == null)
            return;

        var type = shaderApplicationScriptName;

        MonoBehaviour effect = (MonoBehaviour)cam.GetComponent(type);
        Debug.Log(effect.GetType().Name);
        effect.enabled = enabled;

        if (timedEffect)
        {
            // TODO: Shader Manager to disable the shader based on the timer
            ErrorMe("No Manager to turn off the shader yet");
        }

        isPlaying = false;
        
    }

}

//[CustomEditor(typeof(SetCameraEffect))]
//public class SetCameraEffectEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        var myScript = target as SetCameraEffect;

//        base.OnInspectorGUI();

//        if (myScript.timedEffect)
//        {
//            myScript.timeOfEffectOnPlay = EditorGUILayout.FloatField("Timer of Effect",myScript.timeOfEffectOnPlay);

//        }
        
//    }
//}
