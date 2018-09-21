using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCutscene : MonoBehaviour {

    float timeLeft = 115f;
    void Awake()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
