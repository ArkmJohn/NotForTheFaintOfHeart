using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightControl : MonoBehaviour {

    public GameObject flashlight, battery;
    public Image batteryChargeContent;
    public CustomEvent flashOn, flashOff;

    public bool isLightOn = false;
    public float batteryChargeTime = 5.0f;
    public float batteryUseTime = 10.0f;

    public float batteryChargeCounter;
    public float batteryUseCounter;

    void OnEnable()
    {
        battery.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        batteryChargeCounter = 0;
        batteryUseCounter = batteryUseTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && hasChargedBattery())
        {
            UseFlashLight();
        }
        BatteryLogic();
	}

    public void UseFlashLight()
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
        HandleUI();
        // Check if the battery has charge
        if (!hasChargedBattery())
        {
            if (isLightOn)
                TurnOff();
            else
            {
                // If there is no charge charge the battery up
                if (batteryChargeCounter < batteryChargeTime)
                    batteryChargeCounter += Time.deltaTime;
                else
                    FullChargeBattery();
            }
        }

        
        if (hasChargedBattery() && isLightOn)
        {
            // Drain the battery
            BatteryDrain(Time.deltaTime);
        }
        else
        {
            // Charge the battery
            if(batteryUseCounter < batteryUseTime)
                BatteryDrain(-Time.deltaTime / 4);
        }

    }

    void FullChargeBattery()
    {
        batteryUseCounter = batteryUseTime;
        batteryChargeCounter = 0;
    }

    void BatteryDrain(float timeCount)
    {
        if (hasChargedBattery())
        {
            batteryUseCounter -= timeCount;
        }
    }

    void HandleUI()
    {
        float temp = FindValue(batteryUseCounter, 0, batteryUseTime, 0, 1);
        batteryChargeContent.fillAmount = temp;
    }

    float FindValue(float val, float iMin, float iMax, float oMin, float oMax)
    {
        return (val - iMin) * (oMax - oMin) / (iMax - iMin) + oMin;
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
            Debug.Log("No Battery");
            return false;
        }
        else
            return true;
    }


}
