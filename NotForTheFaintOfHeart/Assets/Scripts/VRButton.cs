using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButton : MonoBehaviour
{

    public Image backgroundImage;
    public Color normalColor;
    public Color highlightedColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGazeEnter()
    {
        backgroundImage.color = highlightedColor;
    }

    public void OnGazeExit()
    {
        backgroundImage.color = normalColor;
    }

    public virtual void OnClick()
    {
        Debug.Log("Click");
    }
}
