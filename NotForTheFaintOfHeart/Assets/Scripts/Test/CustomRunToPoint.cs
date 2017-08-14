using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CustomRunToPoint : MonoBehaviour {

    public GameObject endCanvas;
    public PlayGame playGame;

    public void EndGame()
    {
        endCanvas.SetActive(true);
        Player.instance.GetComponent<PlayerController>().canPlay = true;

    }
}
