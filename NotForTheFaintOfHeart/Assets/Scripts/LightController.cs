using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.5f;

    [SerializeField]
    private List<Light> lights = new List<Light>();
    private bool shouldFlicker;
    void Awake()
    {
        foreach (Transform a in transform)
        {
            if(a.gameObject.GetComponent<Light>() != null)
            {
                lights.Add(a.gameObject.GetComponent<Light>());
            }
        }
        
    }

    void Start()
    {
        Flicker(5);
    }

    void Update()
    {
        if (FindDistNearestEnemy() < 5)
        {
            shouldFlicker = true;
        }
        if (shouldFlicker)
        {
            StartCoroutine(Flash2());
        }
        //if (shouldFlicker == false)
        //{
        //    //this.myLight.enabled = false;
        //    foreach (Light a in lights)
        //    {
        //        a.enabled = false;
        //    }
        //}
    }

    public void Flicker(float flickerTimes)
    {
        StartCoroutine(Flash(flickerTimes));
    }

    public void ChangeColor(Color color)
    {
        //this.myLight.color = color;
        foreach (Light a in lights)
        {
            a.color = color;
        }
    }
    [ContextMenu("Turn Lights")]
    public void TurnLights()
    {
        Debug.Log("Turn Lights");

        foreach (Light a in lights)
        {
            a.enabled = ! a.enabled;
        }

    }

    IEnumerator Flash(float flickerTimes)
    {
        float flickerTimeTemp = 0;
        while (flickerTimeTemp < flickerTimes)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            foreach (Light a in lights)
            {
                a.enabled = ! a.enabled;
            }
            flickerTimeTemp++;
        }
        TurnLights();
    }

    IEnumerator Flash2()
    {
        shouldFlicker = false;
        yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        TurnLights();

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
}
