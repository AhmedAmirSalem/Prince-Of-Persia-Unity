using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamSwitcher : MonoBehaviour
{
    public Camera mainCam;
    public Canvas HUDCanvas;
    public GameObject thePrince;
    public AudioSource gameMusic;
    public AudioSource introMusic;
    float timeLeft = 25.56f;
    // private bool isPaused=false;

    /*void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level 1": timeLeft = 25.56f; break;
            case "BossLvl": timeLeft = 25.33f; break;
        }

    }*/
    // Use this for initialization
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level 1": timeLeft = 25.56f; break;
            case "BossLvl": timeLeft = 25.33f; break;
            case "lvl2": timeLeft = 47.07f; break;
        }
        mainCam.GetComponent<Cam>().enabled = false;
        mainCam.enabled = false;
        thePrince.GetComponent<Prince>().enabled = false;
        HUDCanvas.enabled = false;
        gameMusic.Pause();
    }

    void Update()
    {
            if(ingameUI.isPaused)
            if(introMusic.isPlaying)
                introMusic.Pause();
            
        if (!ingameUI.isPaused)
        {
            timeLeft -= Time.deltaTime;
            if (!introMusic.isPlaying) {
                gameMusic.Pause();
                introMusic.UnPause();
            }
            if (timeLeft <= 0 || Input.GetKeyDown(KeyCode.Space))
            {
                mainCam.enabled = true;
                mainCam.GetComponent<Cam>().enabled = true;
                thePrince.GetComponent<Prince>().enabled = true;
                HUDCanvas.enabled = true;
                //GameMaster.isCamIntroEnd = true;
                introMusic.Stop();
                gameMusic.UnPause();
                this.gameObject.SetActive(false);
                this.enabled = false;

            }
        }
    }
}
