using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
    public Canvas mainCanvas;
    public Canvas optionsCanvas;
    public Canvas creditsCanvas;
    public Canvas howToCanvas;
    public Canvas audioCanvas;
    public Slider[] audioSliders; //[music,speech,effects]
    //public AudioSource menuSound;
    public UnityEngine.Audio.AudioMixer mainMixer;
    // Use this for initialization
    public void Awake () { //starts before start
        Time.timeScale = 1;
        optionsCanvas.enabled = false;
        mainCanvas.enabled = true;
        howToCanvas.enabled = false;
        creditsCanvas.enabled = false;
        audioCanvas.enabled = false;
    }
    public void OptionsOn()
    { //starts before start
        optionsCanvas.enabled = true;
        mainCanvas.enabled = false;

    }
    public void ReturnOn()
    { //starts before start
        optionsCanvas.enabled = false;
        mainCanvas.enabled = true;
        howToCanvas.enabled = false;
        creditsCanvas.enabled = false;
        audioCanvas.enabled = false;
    }

    // Update is called once per frame
    public void LoadOn () {
        //Application.LoadLevel(1);
        //SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene("CutScene1");

    }
    public void ExitOn()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
       Application.Quit();
    #endif
    }
    public void ChangeVolume()
    {
        //AudioListener.volume = 0f;
        PlayerPrefs.SetFloat("musicVol", audioSliders[0].value);
        PlayerPrefs.SetFloat("speechVol", audioSliders[1].value);
        PlayerPrefs.SetFloat("effectVol", audioSliders[2].value);
        //menuSound.volume = audioSliders[0].value;
        //PlayerPrefs.GetFloat("MyValue"); // to use volume in your scene use this
        mainMixer.SetFloat("musicVol", audioSliders[0].value);
        mainMixer.SetFloat("speechVol", audioSliders[1].value);
        mainMixer.SetFloat("sfxVol", audioSliders[2].value);
    }
    public void CreditsOn()
    {
        optionsCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }
    public void AudioOn()
    {
        optionsCanvas.enabled = false;
        audioCanvas.enabled = true;
    }
    public void HowToPlayOn()
    {
        optionsCanvas.enabled = false;
        howToCanvas.enabled = true;
    }
}
