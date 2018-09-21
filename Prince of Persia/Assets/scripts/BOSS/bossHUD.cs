using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bossHUD : MonoBehaviour {
    public Slider bossHealthBar;
    bool isGameOver = false;
    float timeLeft = 9.4f;

    public void updateHP()
    {
        try { 
        bossHealthBar.value = bossScript.hp;
        }
        catch (System.Exception)
        {

        }
    }
    // Use this for initialization
	// Update is called once per frame
	void Update () {
        updateHP();
        if (bossHealthBar.value <= 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                SceneManager.LoadScene("EndCredits");
            }
        }
    }
}
