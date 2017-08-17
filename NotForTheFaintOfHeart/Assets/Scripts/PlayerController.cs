using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public bool isHidden = false, hasFlashlight = false;
    public AudioSource heartBeat, heartBeatFast;
    public GameObject interactable, entity, navTarg;
    public GameObject cam;
    public bool isRestart, canPlay;
    public bool isWalking;
    public Rigidbody rb;

    [HideInInspector]
    public float currentTargSpeed;
    public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

    [SerializeField]
    private float movementSpeed = 2f;
    [SerializeField]
    private bool canMove = false;
    [SerializeField]
    private Vector3 moveDir;

    // Use this for initialization
    void Awake () {
         cam = Camera.main.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (FindDistNearestEnemy() > 10 && heartBeat != null && heartBeatFast != null) // Heartbeat Effect
        {
            heartBeat.enabled = true;
            heartBeatFast.enabled = false;
        }
        else if(heartBeat != null && heartBeatFast != null)
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

    void FixedUpdate()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }
        if (player.leftController != null)
        {


            // -- Player Movement -- //
            if (!isHidden )//&& canMove)
            {
                //rb.isKinematic = false;
                Quaternion orient = Camera.main.transform.rotation;
                Vector2 tPadVector = ProcessInput();
                Debug.DrawLine(player.transform.position, moveDir + player.transform.position, Color.red);
                if (tPadVector != Vector2.zero)
                {
                    isWalking = true;
                    Debug.Log("Walking");
                }
                else
                {
                    isWalking = false;
                }

                if ((Mathf.Abs(tPadVector.x) > float.Epsilon || Mathf.Abs(tPadVector.y) > float.Epsilon))
                {
                    Vector3 desVel = cam.transform.forward * tPadVector.y + cam.transform.right * tPadVector.x;
                    desVel.x = desVel.x * currentTargSpeed;
                    desVel.z = desVel.z * currentTargSpeed;
                    desVel.y = desVel.y * currentTargSpeed;

                    rb.AddForce(desVel * SlopeMultiplier(), ForceMode.Impulse);
                    
                }

                if (Mathf.Abs(tPadVector.x) < float.Epsilon && Mathf.Abs(tPadVector.y) < float.Epsilon && rb.velocity.magnitude < 1f)
                {
                    rb.Sleep();
                }
                if (!isWalking)
                {
                    rb.velocity = Vector3.zero;
                    rb.Sleep();
                }
            }
        }
    }

    private float SlopeMultiplier()
    {
        float angle = Vector3.Angle(new Vector3(0, 1, 0), Vector3.up);
        return SlopeCurveModifier.Evaluate(angle);
    }
    Vector2 ProcessInput()
    {
        Player player = Player.instance;

        if (!player)
        {
            return Vector2.zero;
        }

        EVRButtonId tPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
        Vector3 tPadVector = player.leftController.GetAxis(tPad);
        //Debug.Log(tPadVector);
        Vector2 input = new Vector2(tPadVector.x, tPadVector.y);

        if (input.x > 0 || input.x < 0)
        {
            currentTargSpeed = (movementSpeed * 0.7f);
        }
        if (input.y > 0 || input.y < 0)
        {
            currentTargSpeed = (movementSpeed * 0.7f);
        }

        return input;
    }
    void InteractWithInteractable()
    {
        interactable.GetComponent<InteractableObject>().IsFar();

        if (interactable.GetComponent<HidingSpot>() != null)
        {
            if (interactable.GetComponent<HidingSpot>().isHiding == true)
            {
                // Puts the player infront of the hiding spot
                interactable.GetComponent<HidingSpot>().Exit(rb.gameObject);
                rb.isKinematic = false;

                entity.SetActive(true);
                navTarg.SetActive(true);
                isHidden = false;
            }
            else
            {
                // Hides the Player
                interactable.GetComponent<HidingSpot>().Enter(rb.gameObject);
                rb.isKinematic = true;

                entity.SetActive(false);
                navTarg.SetActive(false);

                isHidden = true;
            }
        }
        else
        {
            interactable.GetComponent<InteractableObject>().DoStuff();
        }
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
            if (player.rightController.GetPressDown(SteamVR_Controller.ButtonMask.Grip) || player.leftController.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                if (interactable != null)
                {
                    InteractWithInteractable();
                }
            }
            if (player.rightController.GetPressDown(SteamVR_Controller.ButtonMask.Axis0) || player.leftController.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
            {
                if (isRestart && canPlay)
                {
                    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
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
        if (player.leftController != null)
        {
            if (player.leftController.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
            {
                canMove = true;
            }
            else if (player.leftController.GetPressUp(SteamVR_Controller.ButtonMask.Axis0))
            {
                canMove = false;
            }
            else
                canMove = false;
        }
    }

    void RightHand()
    {
        Player player = Player.instance;

        if (!player)
        {
            return;
        }
        if (null != player.rightController)
        {
            // Uses flashlight
            if (player.rightController.GetPressDown(SteamVR_Controller.ButtonMask.Axis0) && FindObjectOfType<FlashlightControl>().enabled && !isRestart)
            {
                FindObjectOfType<FlashlightControl>().UseFlashLight();
            }


        }
    }

    #endregion

}
