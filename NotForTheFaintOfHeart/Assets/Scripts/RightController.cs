using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RightController : Controller
{

	// Use this for initialization
	void Start ()
    {
        tContDevice.TriggerClicked += OpenFlashLight;
	}

    void OpenFlashLight(object sender, ClickedEventArgs e)
    {
        Debug.Log("Pressed");
    }
}
