using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ingameUI : MonoBehaviour
{

    public Canvas pauseCanvas;
    public Canvas gameOverCanvas;
    public Canvas hudCanvas;
    public Text sandsTxt;
    public Camera mainCam;
    public static bool isPaused = false;
    bool isGameOver = false;
    float timeLeft = 5f;
    public AudioSource inGameSoundTrack;
    public AudioSource MenuSoundTrack;
    /*
    public AudioSource soundTrack;
    public AudioSource victoryTrack;
    public AudioSource loseTrack;
    */
    public Slider healthBar;
    public UnityEngine.Audio.AudioMixer mainMixer;
    public void changeHP(int dHP)
    {
        healthBar.value += dHP;
    }
    public void updateHP()
    {
        healthBar.value = GameMaster.hp;
    }
    public void updateSands()
    {
        sandsTxt.text = "Sands of Time: "+GameMaster.sands.ToString();
    }
    void Awake()
    { //starts before start
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        hudCanvas.enabled = true;
    }
    public void ResumeOn()
    {
        isPaused = false;
        resumeGame();
    }
    public void RestartLevelOn()
    {
        Time.timeScale = 1;
		GameMaster.hp = 100;
		GameMaster.paused = false;
		GameMaster.sands = 0;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ExitOn()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
       Application.Quit();
    #endif
    }
    public void ExitMainMenuOn()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    void muteBackground()
    {
        mainMixer.SetFloat("speechVol", -80f);
        mainMixer.SetFloat("sfxVol", -80f);
    }
    void unMuteBackground()
    {
        mainMixer.SetFloat("speechVol", PlayerPrefs.GetFloat("speech", 0));
        mainMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("effectVol", -10f));

    }
    void pauseGame()
    {
        isPaused = true;
        pauseCanvas.enabled = true;
        //soundTrack.Pause();
        Time.timeScale = 0;
        mainCam.GetComponent<Cam>().enabled = false;
        //GameObject.Find("RigidBodyFPSController").GetComponent<FirstPersonController>().enabled = false;
        muteBackground();
        try { 
        MenuSoundTrack.Play();
        inGameSoundTrack.Pause();
        }
        catch (Exception)
        {

        }
    }
   void resumeGame()
    {
        isPaused = false;
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
        mainCam.GetComponent<Cam>().enabled = true;
        unMuteBackground();
        try
        {

            MenuSoundTrack.Pause();
            inGameSoundTrack.UnPause();
        }
        catch (Exception)
        {

        }
        //soundTrack.UnPause();
        //GameObject.Find("RigidBodyFPSController").GetComponent<FirstPersonController>().enabled = true;

    }
    public void gameOver()
    {
        isPaused = true;
        gameOverCanvas.enabled = true;
        isGameOver = true;
        if (healthBar.value <= 0)
        {
         //   loseTrack.Play();
        }
        Time.timeScale = 0;
       // soundTrack.Stop();



    }
    // Use this for initialization
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        updateHP();
        updateSands();
        if (!(GameMaster.hp <= 0))
        { //not game over 
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    pauseGame();
                }
                else
                {
                    resumeGame();
                }
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                gameOver();
            }
        }


    }

}
