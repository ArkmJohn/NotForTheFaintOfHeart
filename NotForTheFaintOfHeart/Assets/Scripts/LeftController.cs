using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LeftController : Controller
{

	// Use this for initialization
	void Start () {
		
	}

    void Move()
    {

        // Gets the input from the touchpad
        EVRButtonId tPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

        // Orient
        Quaternion ori = Camera.main.transform.rotation;
        Vector2 tPadVect = device.GetAxis(tPad);

        //Vector3 moveDirection = ori * Vector3.forward * ori + tPad.y * Vector3.right * 

    }
}
