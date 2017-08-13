using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Controller : MonoBehaviour {

    public Player player;

    public SteamVR_TrackedController tContDevice;
    public SteamVR_TrackedObject tObjDevice;
    public SteamVR_Controller.Device device;

    public virtual void Awake()
    {
        tContDevice = GetComponent<SteamVR_TrackedController>();
        tObjDevice = GetComponent<SteamVR_TrackedObject>();


    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
