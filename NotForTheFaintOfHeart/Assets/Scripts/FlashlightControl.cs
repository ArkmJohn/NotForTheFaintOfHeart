using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FlashlightControl : MonoBehaviour {

    public GameObject flashlight;
    public CustomEvent flashOn, flashOff;

    public bool isLightOn = false;
    public float batteryChargeTime = 5.0f;
    public float batteryUseTime = 10.0f;

    public float batteryChargeCounter;
    public float batteryUseCounter;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        batteryChargeCounter = 0;
        batteryUseCounter = batteryUseTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            UseFlashLight();
        }
        BatteryLogic();
	}

    void UseFlashLight()
    {
        if (!hasChargedBattery())
            return;

        if (flashlight.GetComponent<Light>().isActiveAndEnabled)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    void BatteryLogic()
    {

        if (!hasChargedBattery() && isLightOn)
        {
            TurnOff();
        }

        // Check if the battery has charge
        if (!hasChargedBattery())
        {
            // If there is no charge charge the battery up
            if (batteryChargeCounter < batteryChargeTime)
                batteryChargeCounter += Time.deltaTime;
            else
                ChargeBattery();
        }

        
        if (isLightOn)
        {
            // Drain the battery
            BatteryDrain(Time.deltaTime);
        }
        else
        {
            // Charge the battery
        }

    }

    void ChargeBattery()
    {
        if (hasChargedBattery())
        {
            batteryUseCounter = batteryUseTime;

        }
    }

    void BatteryDrain(float timeCount)
    {
        if (hasChargedBattery())
        {
            batteryUseTime -= timeCount;
        }
    }

    public void TurnOn()
    {
        flashlight.GetComponent<Light>().enabled = true;
        flashOn.PlayEvent(this.gameObject);
        isLightOn = true;
    }

    public void TurnOff()
    {
        flashlight.GetComponent<Light>().enabled = false;
        flashOff.PlayEvent(this.gameObject);
        isLightOn = false;
    }

    public bool hasChargedBattery()
    {
        if (batteryUseCounter < 0)
        {
            return false;
        }
        else
            return true;
    }

}
