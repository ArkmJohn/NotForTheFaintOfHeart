using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<FlashlightControl>() != null)
            Application.LoadLevel("Exit");


    }
}
