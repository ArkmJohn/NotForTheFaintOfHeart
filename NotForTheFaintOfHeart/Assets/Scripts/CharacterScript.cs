using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterScript : MonoBehaviour {

    #region UtilityClasses

    // Liked the Dropdown fo the RigidbodyFirstPersonController
    [Serializable]
    public class MovementSettings
    {
        [Tooltip("Movement Speed of the Character")]
        public float movementSpeed = 1.0f;
        [Tooltip("Modifier for moving sidewards")]
        public float strafeSpeedModifier = 0.7f;
        [Tooltip("Modifier for walking backward")]
        public float backwardSpeedModifier = 0.5f;
        public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

    }

    #endregion

    #region Public.Variables

    public MovementSettings movementSettings = new MovementSettings();
    [HideInInspector]
    public float currentTargetSpeed = 1;

    #endregion

    #region Private.Variables

    private Rigidbody myRB;
    private Collider myCollider;
    private Camera myCamera;
    #endregion

    #region UnityCallbacks

    void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        Vector2 myInput = ProcessInput();

        if ((Mathf.Abs(myInput.x) > float.Epsilon || Mathf.Abs(myInput.y) > float.Epsilon))
        {
            Vector3 desiredVel = myCamera.transform.forward * myInput.y + myCamera.transform.right * myInput.x;
            desiredVel.x = desiredVel.x * currentTargetSpeed;
            desiredVel.z = desiredVel.z * currentTargetSpeed;
            desiredVel.y = desiredVel.y * currentTargetSpeed;
            //transform.Translate(desiredVel.x * Time.deltaTime, 0, desiredVel.y *Time.deltaTime);
            if(myRB.velocity.sqrMagnitude < currentTargetSpeed * currentTargetSpeed)
                myRB.AddForce(desiredVel * SlopeMultiplier(), ForceMode.Impulse);
        }
        myRB.drag = 5;
        if (Mathf.Abs(myInput.x) < float.Epsilon && Mathf.Abs(myInput.y) < float.Epsilon && myRB.velocity.magnitude < 1f)
        {
            myRB.Sleep();
        }
    }

    #endregion

    #region Public.Functions

    #endregion

    #region Private.Functions

    private Vector2 ProcessInput()
    {
        Vector2 input = new Vector2
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal"),
            y = CrossPlatformInputManager.GetAxis("Vertical")
        };

        if (input.x > 0 || input.x < 0)
        {
            currentTargetSpeed = (movementSettings.movementSpeed * movementSettings.strafeSpeedModifier);
        }
        if (input.y < 0)
        {
            currentTargetSpeed = (movementSettings.movementSpeed * movementSettings.strafeSpeedModifier);
        }

        return input;
    }

    private float SlopeMultiplier()
    {
        float angle = Vector3.Angle(new Vector3(0,1,0), Vector3.up);
        return movementSettings.SlopeCurveModifier.Evaluate(angle);
    }
    #endregion
}
