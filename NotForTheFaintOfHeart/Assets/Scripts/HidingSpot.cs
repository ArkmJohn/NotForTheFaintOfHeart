using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractableObject
{

    public Transform hidenSpot, outsidePos;
    public bool isHiding;

    public override void DoStuff()
    {
        isHiding = true;
    }

    public void Enter(GameObject target)
    {
        target.transform.position = hidenSpot.position;
        target.transform.rotation = hidenSpot.rotation;
    }

    public void Exit(GameObject target)
    {
        target.transform.position = outsidePos.position;
        target.transform.rotation = outsidePos.rotation;
        isHiding = false;
    }

}
