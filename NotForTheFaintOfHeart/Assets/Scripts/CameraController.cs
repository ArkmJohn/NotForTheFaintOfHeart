using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only be used on a test scene without VR
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float sensitivityModifier = 5.0f;
    public float smoothModifier = 1f;

    Vector2 look, smooth;
    GameObject myCharacter;

	// Use this for initialization
	void Start () {
        myCharacter = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mouseD = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseD = Vector2.Scale(mouseD, new Vector2(sensitivityModifier * smoothModifier, sensitivityModifier * smoothModifier));
        smooth.x = Mathf.Lerp(smooth.x, mouseD.x, 1f / smoothModifier);
        smooth.y = Mathf.Lerp(smooth.y, mouseD.y, 1f / smoothModifier);
        look += smooth;
        look.y = Mathf.Clamp(look.y, -60, 60);

        transform.localRotation = Quaternion.AngleAxis(-look.y, Vector3.right);
        myCharacter.transform.localRotation = Quaternion.AngleAxis(look.x, myCharacter.transform.up);
    }
}
