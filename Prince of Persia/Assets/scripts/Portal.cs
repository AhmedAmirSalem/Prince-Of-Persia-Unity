using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider c){

		if(c.gameObject.CompareTag("Player")){

			SceneManager.LoadScene ("lvl2");

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
