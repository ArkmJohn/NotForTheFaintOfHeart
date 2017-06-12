using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance;
    public GameObject losePanel, winPanel;
    GameObject paneltoFade;
    public GameObject pausePanel;

    bool canPause = true;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (paneltoFade != null)
        {
            if (paneltoFade.GetComponent<Image>().color.a > 0)
            {
                FindObjectOfType<CharacterController>().enabled = false;
                FindObjectOfType<OVRSceneSampleController>().enabled = false;
                Color colorAlpha = paneltoFade.GetComponent<Image>().color;
                colorAlpha.a -= Time.deltaTime;
                paneltoFade.GetComponent<Image>().color = colorAlpha;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (canPause)
            {
                DoPause();
            }
        }

    }

    public void DoPause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Test3");
    }

    public void Win()
    {
        canPause = false;
        StartCoroutine(ShowCanvas(winPanel));
    }

    public void Lose()
    {
        canPause = false;

        StartCoroutine(ShowCanvas(losePanel));
    }

    IEnumerator ShowCanvas(GameObject Panel)
    {
        AudioManager.Instance.LowerSound();
        Panel.SetActive(true);
        paneltoFade = Panel.transform.GetChild(2).gameObject;

        return null;
    }
}
