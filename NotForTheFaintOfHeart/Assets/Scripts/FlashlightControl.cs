using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FlashlightControl : MonoBehaviour {

    public GameObject flashlight;
    public CustomEvent flashOn, flashOff;
    public Grayscale grayScale;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            FlashLight();
        }
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    if (Cursor.visible == false)
        //    {
        //        Cursor.visible = true;
        //    }
        //    else
        //        Cursor.visible = false;
        //}
	}

    void FlashLight()
    {
        if (flashlight.GetComponent<Light>().isActiveAndEnabled)
        {
            flashlight.GetComponent<Light>().enabled = false;
            flashOff.PlayEvent(this.gameObject);
            //grayScale.enabled = true;
        }
        else
        {
            flashlight.GetComponent<Light>().enabled = true;
            flashOn.PlayEvent(this.gameObject);

            //grayScale.enabled = false;
        }
    }
}
