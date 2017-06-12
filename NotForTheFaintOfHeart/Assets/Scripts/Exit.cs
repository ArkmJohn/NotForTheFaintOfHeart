using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : InteractableObject
{

    public override void DoStuff()
    {
        GameManager.Instance.Win();
    }
}
