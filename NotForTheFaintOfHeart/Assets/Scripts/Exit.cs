using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FlashlightControl>() != null)
            SceneManager.LoadScene("End", LoadSceneMode.Single);


    }
}
