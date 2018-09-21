using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrap : MonoBehaviour {

	// Use this for initialization

	public AudioSource death;
	bool played;
	void Start () {
		played = false;
	}

	void OnTriggerEnter(){
	
		GameMaster.hp = 0;
		if (!played) {
			death.Play ();
			played = true;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
