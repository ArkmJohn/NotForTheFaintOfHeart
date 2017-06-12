using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public bool isHidden = false;
    public AudioSource heartBeat, heartBeatFast;
    public GameObject interactable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (FindDistNearestEnemy() > 10)
        {
            heartBeat.enabled = true;
            heartBeatFast.enabled = false;
        }
        else
        {
            heartBeat.enabled = false;
            heartBeatFast.enabled = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (interactable != null)
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
        }
	}

    float FindDistNearestEnemy()
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
}
