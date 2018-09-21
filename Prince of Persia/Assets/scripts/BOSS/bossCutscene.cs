using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossCutscene : MonoBehaviour
{
    public Camera mainCam;
    public Camera CMBrain;
    public Canvas HUDCanvas;
    public Canvas bossHUDCanvas;
    public GameObject thePrince;
    public GameObject theBoss;
    public AudioSource gameMusic;
    public UnityEngine.Playables.PlayableDirector cutScene;
    float timeLeft = 124.2f;
    // private bool isPaused=false;

   /* void Awake()
    {
        CMBrain.enabled = true;
        cutScene.Play();
        mainCam.GetComponent<Cam>().enabled = false;
        mainCam.enabled = false;
        thePrince.GetComponent<Prince>().enabled = false;
        HUDCanvas.enabled = false;
        theBoss.GetComponent<bossScript>().enabled = false;
        gameMusic.Pause();
    }*/
    // Use this for initialization
     void Start()
     {
         CMBrain.enabled = true;
         cutScene.Play();
         mainCam.GetComponent<Cam>().enabled = false;
         mainCam.enabled = false;
         thePrince.GetComponent<Prince>().enabled = false;
        GameObject.Find("PrinceAnimator").GetComponent<Animator>().SetFloat("Speed", 0);
		GameObject.Find("PrinceAnimator").GetComponent<Animator>().SetBool("Grounded", true);
		GameObject.Find("PrinceAnimator").GetComponent<Animator>().SetBool("Guard", false);
		thePrince.transform.position = new Vector3 (-24.48862f, 16.59624f, 160.97f);
		thePrince.transform.eulerAngles = new Vector3 (0, -90, 0);
		thePrince.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
        HUDCanvas.enabled = false;
         theBoss.GetComponent<bossScript>().enabled = false;
         gameMusic.Pause();
     }

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.Escape))
        {
           // if (ingameUI.isPaused)
               // introMusic.Pause();

        }*/
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0 || Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.Escape))
            {
                mainCam.enabled = true;
                mainCam.GetComponent<Cam>().enabled = true;
                thePrince.GetComponent<Prince>().enabled = true;
                HUDCanvas.enabled = true;
                gameMusic.UnPause();
                CMBrain.enabled = false;
                theBoss.GetComponent<bossScript>().enabled = true;
                bossHUDCanvas.enabled = true;
                this.gameObject.SetActive(false);
                this.enabled = false;

            }
    }
}
