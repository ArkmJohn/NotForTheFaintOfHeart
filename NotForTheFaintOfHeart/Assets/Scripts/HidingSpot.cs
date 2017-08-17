using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractableObject
{

    public Transform hidenSpot, outsidePos;
    public AudioSource aSource;
    public AudioClip play;
    public bool isHiding;

    public void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    public override void DoStuff()
    {
        Debug.LogError("Missing Target");
    }

    public override void DoStuff(GameObject target)
    {
        if (isHiding)
        {
            Exit(target);
        }
        else
        {
            Enter(target);
        }
    }

    public void Enter(GameObject target)
    {
        target.transform.position = hidenSpot.position;
        target.transform.rotation = hidenSpot.rotation;
        target.GetComponent<Collider>().enabled = false;
        isHiding = true;
        aSource.PlayOneShot(play);
    }

    public void Exit(GameObject target)
    {
        aSource.PlayOneShot(play);
        target.transform.position = outsidePos.position;
        target.transform.rotation = outsidePos.rotation;
        target.GetComponent<Collider>().enabled = true;

        isHiding = false;
    }

}
