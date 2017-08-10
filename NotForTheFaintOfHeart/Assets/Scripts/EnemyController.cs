using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public void EndGame()
    {
        Application.LoadLevel("Exit");
    }

    //public GameObject scratchObject;
    //public AudioClip growl, attackClip;
    //public float maxTimer = 10;

    //float timer = 10;

    //// Use this for initialization
    //void Start() {

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    timer -= Time.deltaTime;
    //    if (timer < 0)
    //    {
    //        AudioManager.Instance.PlayOneSound(growl, GetComponent<AudioSource>());
    //        timer = maxTimer;
    //    }
    //}

    //public void LightFlicker()
    //{
    //    LightController[] allLights = FindObjectsOfType<LightController>();

    //    foreach (LightController l in allLights)
    //    {
    //        l.Flicker(5);
    //    }
    //}

    //void OnColliderEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        EndGame();
    //    }
    //}

    //public void EndGame()
    //{
    //    // Game Should End Here
    //    GameManager.Instance.Lose();
    //}
}
