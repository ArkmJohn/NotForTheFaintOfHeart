using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchControl : InteractableObject {

    [Tooltip ("The Light to be switched on or off.")]
    public LightController myLight;

    public override void DoStuff()
    {
        myLight.TurnLights();
    }




}
