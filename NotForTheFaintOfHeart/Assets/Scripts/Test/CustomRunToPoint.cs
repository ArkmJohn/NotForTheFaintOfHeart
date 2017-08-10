using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRunToPoint : MonoBehaviour {

    public GameObject endCanvas;
    public PlayGame playGame;

    public void EndGame()
    {
        endCanvas.SetActive(true);
        playGame.enabled = true;

    }
}
