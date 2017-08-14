using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour {

    public bool isHidden = false, hasFlashlight = false;
    public AudioSource heartBeat, heartBeatFast;
    public GameObject interactable;
    public GameObject cam;
    public bool isWalking;

    [SerializeField]
    private float movementSpeed = 2f;
    [SerializeField]
    private float vertTurnSpeed = 2f;
    [SerializeField]
    [Range(0, 360)]
    private float horTurnSpeed = 180f;
    [SerializeField]
    private bool turnInvert = false;
    [SerializeField]
    [Range(0, 360)]
    private const float vert_limit = 80f;


	// Use this for initialization
	void Start () {
        cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (FindDistNearestEnemy() > 10) // Heartbeat Effect
        {
            heartBeat.enabled = true;
            heartBeatFast.enabled = false;
        }
        else
        {
            heartBeat.enabled = false;
            heartBeatFast.enabled = true;
        }
        if (Input.GetButtonDown("Fire2")) // Interactable Fuction
        {
            if (interactable != null)
            {
                InteractWithInteractable();
            }
        }

        if (hasFlashlight)
        {
            GetComponent<FlashlightControl>().enabled = true;
        }

        AnyHand();
        LeftHand();
        RightHand();
	}

    void InteractWithInteractable()
    {
        interactable.GetComponent<InteractableObject>().IsFar();

        if (interactable.GetComponent<HidingSpot>() != null)
        {
            if (interactable.GetComponent<HidingSpot>().isHiding == true)
            {
                // Puts the player infront of the hiding spot
                interactable.GetComponent<HidingSpot>().Exit(this.gameObject);

                GetComponent<CharacterController>().enabled = true;
                isHidden = false;
            }
            else
            {
                // Hides the Player
                interactable.GetComponent<HidingSpot>().Enter(this.gameObject);
                interactable.GetComponent<InteractableObject>().DoStuff();

                GetComponent<CharacterController>().enabled = false;
                isHidden = true;
            }
        }
        else
        {
            interactable.GetComponent<InteractableObject>().DoStuff();
        }
    }

    float Angle(float input)
    {
        if (input < 0f)
        {
            return -Mathf.LerpAngle(0, vert_limit, -input);
        }
        else if (input > 0f)
        {
            return Mathf.LerpAngle(0, vert_limit, input);
        }
        return 0f;
    }

    float FindDistNearestEnemy() // Detects the nearest enemy to be used for indicators
    {
        EnemyController[] allEnemies = FindObjectsOfType<EnemyController>();
        float dist = 9999;
        GameObject enemyObject = null;

        foreach (EnemyController a in allEnemies)
        {
            float distanceTemp = Vector3.Distance(a.gameObject.transform.position, this.gameObject.transform.position);
            if (distanceTemp < dist)
            {
                enemyObject = a.gameObject;
                dist = distanceTemp;
            }
           
        }

        return dist;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (interactable != null && interactable != other)
            {
                interactable.GetComponent<InteractableObject>().IsFar();
                interactable = other.gameObject;
                interactable.GetComponent<InteractableObject>().IsNear();
            }
            else
            {
                interactable = other.gameObject;
                interactable.GetComponent<InteractableObject>().IsNear();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactable.GetComponent<InteractableObject>().IsFar();
            interactable = null;

        } 
    }

    #region Controller Logic

    void AnyHand()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }

        // Lets the player interact with other stuff
        if (player.leftController != null || player.rightController != null)
        {
            if (player.rightController.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) || player.leftController.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (interactable != null)
                {
                    InteractWithInteractable();
                }
            }
        }
    }

    void LeftHand()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }

        EVRButtonId tPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

        if (player.leftController != null)
        {

            // -- Player Movement -- //
            Quaternion orient = Camera.main.transform.rotation;
            Vector3 tPadVector = player.leftController.GetAxis(tPad);
            Vector3 moveDir = orient * Vector3.forward * tPadVector.y + orient * Vector3.right * tPadVector.x;
            if (tPadVector != Vector3.zero)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
            Vector3 playerPos = player.transform.position;
            playerPos.x += moveDir.x * movementSpeed * Time.deltaTime;
            playerPos.z += moveDir.z * movementSpeed * Time.deltaTime;
            player.transform.position = playerPos;
            // -- End Player Movement Section -- //

        }
    }

    void RightHand()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }

        EVRButtonId tPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

        if (null != player.rightController)
        {
            // -- Player Rotation -- //
            /*--
            // Finds the Orientation of the player
            Quaternion orient = player.transform.rotation;
            // Gets the vector from the input
            Vector2 tPadVector = player.rightController.GetAxis(tPad);
            // Gets the rotation of this onbject
            Vector3 eAngle = transform.rotation.eulerAngles;

            // Gets the Angle
            float angle;
            if (turnInvert)
            {
                angle = Angle(tPadVector.y);
            }
            else
            {
                angle = Angle(-tPadVector.y);
            }

            // Sets the next angle to the player
            eAngle.x = Mathf.LerpAngle(eAngle.x, angle, vertTurnSpeed * Time.deltaTime);
            eAngle.y += tPadVector.x * horTurnSpeed * Time.deltaTime;
            player.transform.rotation = Quaternion.Euler(eAngle);
            --*/
            // -- End Player Rotation Section -- //

            // Uses flashlight
            if (player.rightController.GetPressDown(SteamVR_Controller.ButtonMask.Axis0) && FindObjectOfType<FlashlightControl>().enabled)
            {
                FindObjectOfType<FlashlightControl>().UseFlashLight();
            }


        }
    }

    #endregion

    void OnDrawGizmos()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }

        EVRButtonId tPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
        Vector3 moveDir = Vector3.zero;
        if (player.leftController != null)
        {
            // -- Player Movement -- //
            // Get player Orientation
            Quaternion orient = Camera.main.transform.rotation;

            // Gets the vector from the input
            Vector2 tPadVect = player.leftController.GetAxis(tPad);
            // Gets the direction
            moveDir = orient * Vector3.forward * tPadVect.y + orient * Vector3.right * tPadVect.x;
        }
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(cam.transform.rotation * Vector3.one + cam.transform.position, 1);
        //Gizmos.DrawLine(cam.transform.position, cam.transform.rotation * Vector3.forward);
    }
}
