using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightInteractable : InteractableObject {

    public GameObject instructions;

    public override void DoStuff()
    {
        FindObjectOfType<PlayerController>().hasFlashlight = true;
        instructions.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void DoStuff(GameObject target)
    {
        Debug.LogError("No Need for target");
    }

}
