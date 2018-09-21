using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour {
    //public GameObject CreditsGroup;
    float timeLeft = 269.6f;//60 //45
    void Awake()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
            timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
