using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    public PlayerController playerController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (playerController.interactable != null && playerController.interactable != other)
            {
                playerController.interactable.GetComponent<InteractableObject>().IsFar();
                playerController.interactable = other.gameObject;
                playerController.interactable.GetComponent<InteractableObject>().IsNear();
            }
            else
            {
                playerController.interactable = other.gameObject;
                playerController.interactable.GetComponent<InteractableObject>().IsNear();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            playerController.interactable.GetComponent<InteractableObject>().IsFar();
            playerController.interactable = null;

        }
    }
}
