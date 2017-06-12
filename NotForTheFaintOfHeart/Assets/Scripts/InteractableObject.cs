using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public GameObject nearIndicator;

    public abstract void DoStuff();

    public virtual void IsNear()
    {
        nearIndicator.SetActive(true);
    }

    public virtual void IsFar()
    {
        nearIndicator.SetActive(false);
    }
}
