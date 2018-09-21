using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class portal2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


	void OnTriggerEnter(Collider c){

		if(c.gameObject.CompareTag("PrinceWeapon")){


			SceneManager.LoadScene ("BossLvl");

		}

	}


	// Update is called once per frame
	void Update () {
		
	}
}
